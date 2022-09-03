using Microsoft.AspNetCore.Builder;
using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;
using Microsoft.AspNetCore.Hosting;

namespace Workflow.Api
{
    public static class ConfigureExtensions
    {
        /// <summary>
        /// swagger-ui
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggUIConfigure(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseKnife4UI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/v1/api-docs", "V1 Docs");
            });
        }

        public static IApplicationBuilder UseWorkflowConfigure(this IApplicationBuilder app)
        {
            var host = app.ApplicationServices.GetService<IWorkflowHost>();
            //host.RegisterWorkflow<EdcWorkflow>();
            //host.RegisterWorkflow<EdcDataWorkflow, EdcData>();
            host.Start();

            var appLifetime = app.ApplicationServices.GetService<IApplicationLifetime>();
            appLifetime.ApplicationStopping.Register(() =>
            {
                host.Stop();
            });

            return app;
        }
    }
}
