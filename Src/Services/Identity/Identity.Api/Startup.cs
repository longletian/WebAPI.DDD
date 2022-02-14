using Autofac;
using InfrastructureBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Identity.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostEnvironment Env { get; }
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new AppSettingConfig(Configuration,Env));
            services.AddControllService();
            services.AddFreeSqlService();
            services.AddCorsService();
            services.AddDaprClient();
            services.AddSwaggUIService();
        }

        /// <summary>
        /// ×¢Èëautofac
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutoFacModule());
            builder.RegisterModule(new DependencyModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseSwaggUIConfigure();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSwagger("{documentName}/api-docs");
            });
        }
    }
}
