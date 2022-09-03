using DomainBase;
using FreeSql;
using FreeSql.DataAnnotations;
using InfrastructureBase;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Infrastructure
{
    public static class FreeSqlExtension
    {
        public static ISelect<T> AsTable<T>(this ISelect<T> @this, params string[] tableNames) where T : class
        {
            tableNames?.ToList().ForEach(tableName =>
            {
                @this.AsTable((type, oldname) =>
                {
                    if (type == typeof(T)) return tableName;
                    return null;
                });
            });
            return @this;
        }

        /// <summary>
        /// 配置数据库连接
        /// </summary>
        /// <param name="this"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static FreeSqlBuilder UseConnectionString(this FreeSqlBuilder @this)
        {
            IConfigurationSection dbTypeCode = AppSettingConfig.GetSection("ConnectionStrings:DefaultDB");
            if (Enum.TryParse(dbTypeCode.Value, out DataType dataType))
            {
                if (!Enum.IsDefined(typeof(DataType), dataType))
                {
                    Log.Error($"数据库配置ConnectionStrings:DefaultDB:{dataType}无效");
                }

                IConfigurationSection configurationSection =
                    AppSettingConfig.GetSection($"ConnectionStrings:{dataType}");
                @this.UseConnectionString(dataType, configurationSection.Value);
                if (AppSettingConfig.GetSection("ConnectionStrings:BoolOpenSalve").Value == "true")
                {
                    string dbStr = AppSettingConfig.GetSection("ConnectionStrings:SalveDB").Value;
                    if (!string.IsNullOrEmpty(dbStr))
                    {
                        if (dbStr.IndexOf(',') > 0)
                        {
                            string[] arrayStr = dbStr.Split(',');
                            List<string> lists = new List<string>();
                            for (int i = 0; i < arrayStr.Length; i++)
                            {
                                lists.Add(AppSettingConfig.GetSection($"ConnectionStrings:{arrayStr[i]}").Value);
                            }

                            @this.UseSlave(lists.ToArray());
                        }
                        else
                        {
                            @this.UseSlave(AppSettingConfig.GetSection($"ConnectionStrings:{dbStr}").ToString());
                        }
                    }
                }
            }
            else
            {
                Log.Error($"数据库配置ConnectionStrings:DefaultDB:{dbTypeCode.Value}无效");
            }

            return @this;
        }

        /// <summary>
        /// 请在UseConnectionString配置后调用此方法
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static FreeSqlBuilder CreateDatabaseIfNotExists(this FreeSqlBuilder @this)
        {
            FieldInfo dataTypeFieldInfo =
                @this.GetType().GetField("_dataType", BindingFlags.NonPublic | BindingFlags.Instance);

            if (dataTypeFieldInfo is null)
            {
                throw new ArgumentException("_dataType is null");
            }

            string connectionString = GetConnectionString(@this);
            DataType dbType = (DataType)dataTypeFieldInfo.GetValue(@this);

            switch (dbType)
            {
                case DataType.MySql:
                    return @this.CreateDatabaseIfNotExistsMySql(connectionString);
                case DataType.PostgreSQL:
                    //return @this.CreateDatabaseIfNotExistsPostgreSql(connectionString);
                    break;
                default:
                    break;
            }

            //Log.Error($"不支持创建数据库");
            return @this;
        }

        #region mysql创建数据库
        public static FreeSqlBuilder CreateDatabaseIfNotExistsMySql(this FreeSqlBuilder @this,
            string connectionString = "")
        {
            if (connectionString == "")
            {
                connectionString = GetConnectionString(@this);
            }

            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder(connectionString);

            string createDatabaseSql =
                $"USE mysql;CREATE DATABASE IF NOT EXISTS `{builder.Database}` CHARACTER SET '{builder.CharacterSet}' COLLATE 'utf8mb4_general_ci'";

            using MySqlConnection cnn = new MySqlConnection(
                $"Data Source={builder.Server};Port={builder.Port};User ID={builder.UserID};Password={builder.Password};Initial Catalog=mysql;Charset=utf8;SslMode=none;Max pool size=1");

            cnn.Open();

            using (MySqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandText = createDatabaseSql;
                cmd.ExecuteNonQuery();
            }

            return @this;
        }

        #endregion

        private static string ExpandFileName(string fileName)
        {
            if (fileName.StartsWith("|DataDirectory|", StringComparison.OrdinalIgnoreCase))
            {
                var dataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
                if (string.IsNullOrEmpty(dataDirectory))
                {
                    dataDirectory = AppDomain.CurrentDomain.BaseDirectory;
                }

                string name = fileName.Replace("\\", "").Replace("/", "").Substring("|DataDirectory|".Length);
                fileName = Path.Combine(dataDirectory, name);
            }

            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            }

            return Path.GetFullPath(fileName);
        }

        /// <summary>
        /// 多库问题
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        private static string GetConnectionString(FreeSqlBuilder @this)
        {
            Type type = @this.GetType();
            FieldInfo fieldInfo =
                type.GetField("_masterConnectionString", BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfo is null)
            {
                throw new ArgumentException("_masterConnectionString is null");
            }

            return fieldInfo.GetValue(@this).ToString();
        }

        public static ICodeFirst SeedData(this ICodeFirst @this)
        {
            //@this.Entity<RoleEntity>(e =>
            //{
            //    e.HasData(new List<RoleEntity>
            //    {
            //        new RoleEntity {SortNum = 1, ParentId = null, RoleName = "管理员", Description = ""},
            //        new RoleEntity {SortNum = 2, ParentId = null, RoleName = "城管", Description = ""}
            //    });
            //});

            return @this;
        }

        public static Type[] GetTypesByTableAttribute()
        {
            List<Type> tableAssembies = new List<Type>();
            foreach (Type type in Assembly.GetAssembly(typeof(Entity)).GetExportedTypes())
            {
                foreach (Attribute attribute in type.GetCustomAttributes())
                {
                    if (attribute is TableAttribute tableAttribute)
                    {
                        if (tableAttribute.DisableSyncStructure == false)
                        {
                            tableAssembies.Add(type);
                        }
                    }
                }
            }

            ;
            return tableAssembies.ToArray();
        }

        /// <summary>
        /// 反射获取
        /// </summary>
        /// <returns></returns>
        public static Type[] GetTypesByNameSpace()
        {
            List<Type> tableAssembies = new List<Type>();
            Assembly assembly = Assembly.Load("Workflow.Infrastructure");
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAssignableTo(typeof(Entity)) || type.IsAssignableTo(typeof(PersistenceObjectBase)))
                {
                    foreach (Attribute attribute in type.GetCustomAttributes())
                    {
                        if (attribute is TableAttribute tableAttribute)
                        {
                            if (tableAttribute.DisableSyncStructure == false)
                            {
                                tableAssembies.Add(type);
                            }
                        }
                    }
                }
            }
            return tableAssembies.ToArray();
        }
    }
}
