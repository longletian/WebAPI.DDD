using FreeSql;
using FreeSql.Internal;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using InfrastructureBase;

namespace Identity.Api
{
    public static class ServiceCollectionExtenioncs
    {
        public static void AddFreeSqlService(this IServiceCollection services)
        {
            IFreeSql freeSql = new FreeSqlBuilder()
               .UseGenerateCommandParameterWithLambda(true)
               .UseConnectionString(DataType.MySql,"")
               //定义名称格式
               .UseNameConvert(NameConvertType.PascalCaseToUnderscoreWithLower)
               .UseMonitorCommand(cmd =>
               {
                   Log.Information(cmd.CommandText + ";");
               })
               //自动同步实体结构到数据库
               .UseAutoSyncStructure(false) 
               .Build(); //请务必定义成 Singleton 单例模式
              
            services.AddSingleton(freeSql);
        }

        /// <summary>
        /// 注入跨域
        /// </summary>
        /// <param name="services"></param>
        public static void AddCorsService(this IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                    // 允许浏览器应用进行跨域 gRPC-Web 调用
                    //.WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding")
                    //.WithExposedHeaders(tusdotnet.Helpers.CorsHelper.GetExposedHeaders());
            }));
        }



        /// <summary>
        /// 添加控制器数据验证
        /// </summary>
        /// <param name="services"></param>
        public static void AddControllService(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(option => option.JsonSerializerOptions.PropertyNamingPolicy = null)
                .AddFluentValidation(fv =>
                {
                    //是否同时支持两种验证方式
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    //自定义IValidator验证空接口
                    fv.RegisterValidatorsFromAssemblyContaining<IValidator>();
                });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState
                        .Values
                        .SelectMany(x => x.Errors
                            .Select(p => p.ErrorMessage))
                        .ToList();

                    var result = new
                    {
                        Code = "405",
                        Message = "Validation errors",
                        Errors = errors
                    };
                    return new BadRequestObjectResult(result);
                };
            });
        }


        /// <summary>
        /// 注入swaggerui
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggUIService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "xx",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                // 引入Swashbuckle和FluentValidation
                c.AddFluentValidationRules();
            });
        }

    }
}
