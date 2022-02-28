using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace InfrastructureBase
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public class AppSettingConfig
    {
        static IConfiguration Configuration { get; set; }
        static IHostEnvironment Env { get; set; }
        public AppSettingConfig(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }
        
        /// <summary>
        /// Connectionstrings下面的数据 
        /// </summary>
        /// <param name="conStr"></param>
        /// <returns></returns>
        public static string GetConnStrings(string conStr)
        {
            if (!string.IsNullOrEmpty(conStr))
            {
                return Configuration.GetConnectionString(conStr);
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
        public static T BindSection<T>(string sectionConstr)
        {
            if (!string.IsNullOrEmpty(sectionConstr))
            {
                if (Configuration.GetSection(sectionConstr).Exists())
                {
                    return Configuration.GetValue<T>(sectionConstr);
                }
            }
            return default(T);
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
