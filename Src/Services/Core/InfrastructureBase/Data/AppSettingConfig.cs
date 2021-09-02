using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InfrastructureBase
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public class AppSettingConfig:ISingletonDependency
    {
        static IConfiguration Configuration { get; set; }
        static IHostEnvironment Env { get; set; }
        public AppSettingConfig(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public AppSettingConfig()
        {
            Configuration = new ConfigurationBuilder()
               .SetBasePath(Env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build();
        }

        /// <summary>
        /// Connectionstrings下面的数据 
        /// </summary>
        /// <param name="constr"></param>
        /// <returns></returns>
        public static string GetConnStrings(string constr)
        {
            if (!string.IsNullOrEmpty(constr))
            {
                return Configuration.GetConnectionString(constr);
            }
            return "";
        }

        /// <summary>
        /// 格式 "JsonConfig:JsonName" 每下一层用：
        /// </summary>
        /// <param name="sectionConstr"></param>
        /// <returns></returns>
        public static IConfigurationSection GetSection(string sectionConstr)
        {
            if (!string.IsNullOrEmpty(sectionConstr))
            {
                if (Configuration.GetSection(sectionConstr).Exists())
                {
                    return Configuration.GetSection(sectionConstr);
                }
            }
            return Configuration.GetSection("");
        }

        /// <summary>
        /// 绑定实体类,使用需要注入使用
        /// </summary>
        /// <param name="sectionConstr"></param>
        /// <returns></returns>
        public static void BindSection<T>(string sectionConstr, T entity)
        {
            if (!string.IsNullOrEmpty(sectionConstr))
            {
                if (Configuration.GetSection(sectionConstr).Exists())
                {
                    Configuration.GetSection(sectionConstr).Bind(entity);
                }
            }
        }

        /// <summary>
        /// 判断文件夹下面是否存在配置文件了
        /// </summary>
        /// <param name="path"></param>
        public void DirectoryExistsConfigFile(string  path)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = Directory.GetCurrentDirectory();
            }
            DirectoryInfo root = new DirectoryInfo(path);
            FileInfo[] files = root.GetFiles();
        }
    }
}
