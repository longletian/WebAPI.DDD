using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace Identity.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Log.Information("system init start");

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(Configuration)
                    .CreateLogger();

                CreateHostBuilder(args).Build().Run();

                Log.Information("system init end");
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
                    .UseUrls("http://*:6868;https://*:6888")
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
            .AddJsonFile("appsettings.json",optional:true,reloadOnChange:true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        #endregion
    }
}
