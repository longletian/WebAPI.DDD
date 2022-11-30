using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Serilog;
using InfrastructureBase;
using Microsoft.EntityFrameworkCore;
using Workflow.Api.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection.Extensions;

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

                var builder = WebApplication.CreateBuilder(args);

                builder.Services.AddSingleton(new AppSettingConfig(Configuration, builder.Environment));
                var connectionString = AppSettingConfig.GetConnStrings("MysqlWorkCon");

                // 多租户时候
                builder.Services
                    .AddDbContextFactory<WorkContext>(options =>
                       options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                        //  options.UseSqlite(connectionString) 
                        );

                builder.Services.AddCorsService();

                builder.Services.AddFreeSqlService();

                builder.Services.AddWorkflowCoreElsaService(builder.Environment);

                builder.WebHost.
                   UseSerilog()
                  .UseConfiguration(Configuration)
                      //.UseUrls("http://*:10001")
                     .ConfigureKestrel((context, options) =>
                     {
                         options.Limits.MaxRequestBodySize = 52428800;
                     });

                builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
                {
                    // builder.RegisterModule(new AutoFacModule());
                    builder.RegisterModule(new DependencyModule());
                }).UseServiceProviderFactory(new AutofacServiceProviderFactory());

                var app = builder.Build();
                app.UseCors();
                app.UseStaticFiles();
                app.UseSerilogRequestLogging();
                app.UseHttpActivities();

                app.MapControllers();
                // endpoints.MapRazorPages();
                // 在.net 5中FallbackToPage必须是razor page而不是razor视图
                app.MapFallbackToPage("/Login");

                app.Run();

                Log.Information("system init end");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host builder error");
                throw new Exception($"{ex.Message}");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        #region.net5
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //  Host.CreateDefaultBuilder(args)
        //     // 注意点 ConfigureServices在这里是无效的 
        //     .ConfigureWebHostDefaults((webBuilder) =>
        //     {
        //         webBuilder
        //             .ConfigureServices((hostContext, services) =>
        //             {
        //                 services.AddSingleton(new AppSettingConfig(Configuration, hostContext.HostingEnvironment));
        //                 var connectionString = AppSettingConfig.GetConnStrings("MysqlWorkCon");

        //                 // 多租户时候
        //                 services
        //                     .AddDbContextFactory<WorkContext>(options =>
        //                        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        //                         //  options.UseSqlite(connectionString)
        //                         );

        //                 services.AddCorsService();

        //                 services.AddFreeSqlService();

        //                 services.AddWorkflowCoreElsaService(hostContext.HostingEnvironment);

        //             })
        //             .Configure((app) =>
        //             {
        //                 // app.UseCommonConfigure();
        //                 app.UseCors();
        //                 app.UseStaticFiles();
        //                 app.UseSerilogRequestLogging();
        //                 app.UseHttpActivities();
        //                 app.UseRouting();
        //                 // app.UseSwaggUIConfigure();
        //                 app.UseEndpoints(endpoints =>
        //                 {
        //                     endpoints.MapControllers();
        //                     // endpoints.MapRazorPages();
        //                     // 在.net 5中FallbackToPage必须是razor page而不是razor视图
        //                     endpoints.MapFallbackToPage("/Login");

        //                     // Notifications
        //                     // endpoints.MapBlazorHub();
        //                     // endpoints.MapHub<WorkflowInstanceInfoHub>("/usertask-info");
        //                 });
        //             })
        //             .UseSerilog()
        //             .UseConfiguration(Configuration)
        //             // .UseUrls("http://*:10001")
        //             .ConfigureKestrel((context, options) =>
        //             {
        //                 options.Limits.MaxRequestBodySize = 52428800;
        //             });
        //     }).ConfigureContainer<ContainerBuilder>(builder =>
        //     {
        //         // builder.RegisterModule(new AutoFacModule());
        //         builder.RegisterModule(new DependencyModule());
        //     }).UseServiceProviderFactory(new AutofacServiceProviderFactory());
        #endregion

        #region 配置读取
        private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
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
