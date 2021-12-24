using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using Microsoft.Extensions.Configuration.Ini;

namespace Identity.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(Configuration)
                    .CreateLogger();

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host builder error");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args) 
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseSerilog()
                    .UseConfiguration(Configuration)
                    .ConfigureKestrel((context, options) =>
                    {
                        options.Limits.MaxRequestBodySize = 52428800;
                    });
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory());
        
        #region 配置读取
        /// <summary>
        /// 自定义配置文件读取
        /// </summary>
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "JsonConfig"))
            .AddJsonFile("appsettings_log.json", optional: true, reloadOnChange: true)
            .AddJsonFile("dbsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        #endregion
    }
}
