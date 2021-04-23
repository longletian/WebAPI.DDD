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
                // ��Ҫ�ͷ�
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseSerilog()
                    .ConfigureKestrel((context, options) =>
                    {
                        options.Limits.MaxRequestBodySize = 52428800;
                    });
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory());

        #region ���ö�ȡ
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Path.Combine( Directory.GetCurrentDirectory(), "JsonConfig"))
            .AddJsonFile("appsettings_log.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        #endregion
    }
}