using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGeekFan.AspNetCore.Knife4jUI;

namespace Identity.Api
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
                c.RoutePrefix = ""; // serve the UI at root
                c.SwaggerEndpoint("/v1/api-docs", "V1 Docs");
            });
        }
    }
}
