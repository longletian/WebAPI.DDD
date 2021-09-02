using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace Workflow.Api
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                //Log.Logger = new LoggerConfiguration()
                //    .ReadFrom.Configuration(Configuration)
                //    .CreateLogger();
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                //Log.Fatal(ex, "Host builder error");
            }
            finally
            {
                // 需要释放
                //Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .ConfigureServices((service) =>
                    {

                    })
                    .Configure((app) =>
                    {

                    });

                }).ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new AutoFacModule());
                    builder.RegisterModule(new DependencyModule());
                }).UseServiceProviderFactory(new AutofacServiceProviderFactory());

        #region 配置读取
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings_log.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        #endregion
    }
}
