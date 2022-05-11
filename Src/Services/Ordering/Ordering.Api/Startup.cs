using Autofac;
using InfrastructureBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ordering.Domain.IntegrationEvents;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Api
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
            services.AddSingleton(new AppSettingConfig(Configuration, Env));
            services.AddControllService();
            services.AddCommonService();
            //services.AddFreeSqlService();
            services.AddCorsService();
            services.AddAuthenticationService();
            services.AddAuthorizationService();
            services.AddSwaggUIService();
            //services.AddHealthCheckService();
            services.AddRabbitmqService();
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
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseSwaggUIConfigure();
            app.UseCloudEvents();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
                //endpoints.MapSubscribeHandler();
                endpoints.MapSwagger("{documentName}/api-docs");
            });

            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<OrderEventHandle, OrderEvent>(nameof(OrderEvent), env.ApplicationName);
        }

    }
}
