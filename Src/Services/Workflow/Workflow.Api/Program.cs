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
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Workflow.Api.Infrastructure.Data;

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
                throw new Exception($"{ex.Message}");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // 注意点 ConfigureServices在这里是无效的 
               .ConfigureWebHostDefaults((webBuilder) =>
               {
                   webBuilder
                       .ConfigureServices((hostContext, services) =>
                       {
                           services.AddSingleton(new AppSettingConfig(Configuration, hostContext.HostingEnvironment));
                           var connectionString = AppSettingConfig.GetConnStrings("MysqlWorkCon");

                           // 多租户时候
                           services
                               .AddDbContextFactory<WorkContext>(options =>
                                   options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

                           services.AddCorsService();

                           services.AddFreeSqlService();

                           services.AddWorkflowCoreElsaService(hostContext.HostingEnvironment)
                               .AddCustomWorkflowActivitiesService();
                       })
                       .Configure((app) =>
                       {
                           // app.UseCommonConfigure();
                           app.UseStaticFiles();
                           app.UseCors();
                           app.UseHttpActivities();
                           app.UseSerilogRequestLogging();
                           app.UseRouting();
                           app.UseEndpoints(endpoints =>
                           {
                               endpoints.MapControllers();
                               // endpoints.MapRazorPages();
                               // 在.net 5中FallbackToPage必须是razor page而不是razor视图
                               endpoints.MapFallbackToPage("/Index");
                           });
                       })
                       .UseSerilog()
                       .UseConfiguration(Configuration)
                       .UseUrls("http://*:10001")
                       .ConfigureKestrel((context, options) =>
                       {
                           options.Limits.MaxRequestBodySize = 52428800;
                       });
               }).ConfigureContainer<ContainerBuilder>(builder =>
                {
                    // builder.RegisterModule(new AutoFacModule());
                    builder.RegisterModule(new DependencyModule());
                }).UseServiceProviderFactory(new AutofacServiceProviderFactory());

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
