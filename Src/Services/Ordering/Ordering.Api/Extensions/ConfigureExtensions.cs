using Microsoft.AspNetCore.Builder;
using IGeekFan.AspNetCore.Knife4jUI;

namespace Ordering.Api
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
    }
}
