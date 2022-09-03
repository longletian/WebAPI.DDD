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
using Serilog;
using InfrastructureBase;

namespace Workflow.Api
{
    class Program
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
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddLogging();
                   services.AddSingleton(new AppSettingConfig(Configuration, hostContext.HostingEnvironment));
                   services.AddWorkFlowService();
               })
                .ConfigureWebHostDefaults((webBuilder) =>
                {
                    webBuilder
                    .Configure((app) =>
                    {
                        app.UseWorkflowConfigure();
                    })
                    .UseSerilog()
                    .UseConfiguration(Configuration)
                    .UseUrls("http://*:7878")
                    .ConfigureKestrel((context, options) =>
                    {
                        options.Limits.MaxRequestBodySize = 52428800;
                    });

                }).ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new AutoFacModule());
                    builder.RegisterModule(new DependencyModule());
                }).UseServiceProviderFactory(new AutofacServiceProviderFactory());

        #region 配置读取
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
         .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "JsonConfig"))
         .AddJsonFile("appsettings_log.json", optional: true, reloadOnChange: true)
         .AddJsonFile("dbsettings.json", optional: true, reloadOnChange: true)
         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
         .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
         .AddEnvironmentVariables()
         .Build();
        #endregion
    }
}
