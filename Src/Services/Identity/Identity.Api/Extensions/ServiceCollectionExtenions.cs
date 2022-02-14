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
using Microsoft.AspNetCore.Mvc;
using Identity.Infrastructure;
using InfrastructureBase;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Api
{
    public static class ServiceCollectionExtenions
    {
        /// <summary>
        /// 添加FreeSql读库
        /// </summary>
        /// <param name="services"></param>
        public static void AddFreeSqlService(this IServiceCollection services)
        {
            IFreeSql freeSql = new FreeSqlBuilder()
               .UseGenerateCommandParameterWithLambda(true)
               .UseConnectionString(DataType.MySql, AppSettingConfig.GetConnStrings("MysqlCon"))
               //定义名称格式
               .UseNameConvert(NameConvertType.PascalCaseToUnderscoreWithLower)
               .UseMonitorCommand(cmd =>
               {
                   Log.Information(cmd.CommandText + ";");
               })
               //自动同步实体结构到数据库
               .UseAutoSyncStructure(true)
               .Build(); //请务必定义成 Singleton 单例模式

            services.AddSingleton(freeSql);
            services.AddFreeRepository();
            //在运行时直接生成表结构
            try
            {
                freeSql.CodeFirst
                    .SeedData()
                    .SyncStructure(FreeSqlExtension.GetTypesByNameSpace());
            }
            catch (Exception e)
            {
                Log.Logger.Error(e + e.StackTrace + e.Message + e.InnerException);
            }
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
                    //fv.RunDefaultMvcValidationAfterFluentValidationExecutes = true;
                    //自定义IValidator验证空接口
                    fv.RegisterValidatorsFromAssemblyContaining<InfrastructureBase.IValidator>();
                });

            //验证统一处理
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState
                        .Values
                        .SelectMany(x => x.Errors
                            .Select(p => p.ErrorMessage))
                        .ToList();

                    var result = new ResponseData
                    {
                        MsgCode = -1,
                        Message = "Validation errors",
                        Data = errors
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API V1", Version = "v1" });
                c.AddServer(new OpenApiServer()
                {
                    Description = "vvv"
                });
                c.CustomOperationIds(apiDesc =>
                {
                    var controllerAction = apiDesc.ActionDescriptor as ControllerActionDescriptor;
                    return controllerAction.ControllerName + "-" + controllerAction.ActionName;
                });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Identity.Api.xml"), true);
                // 引入Swashbuckle和FluentValidation
                c.AddFluentValidationRules();
            });
        }


        /// <summary>
        /// 添加授权服务配置
        /// </summary>
        /// <param name="services"></param>
        public static void AddAuthorizationService(this IServiceCollection services)
        {
            // 以下四种常见的授权方式。

            // 1、这个很简单，其他什么都不用做， 只需要在API层的controller上边，增加特性即可
            // [Authorize(Roles = "Admin,System")]

            // 2、这个和上边的异曲同工，好处就是不用在controller中，写多个 roles 。
            // 然后这么写 [Authorize(Policy = "Admin")]
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
            });

            // 3、自定义复杂的策略授权

            // 4.


        }



        /// <summary>
        /// 添加授权配置
        /// </summary>
        /// <param name="services"></param>
        public static void AddAuthenticationService(this IServiceCollection services)
        {
            services.AddAuthentication((s) =>
            {
                s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer((x) =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    SaveSigninToken = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidIssuer = "",
                    ValidAudience = "",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingConfig.GetSection("").ToString())),
                    ClockSkew = TimeSpan.FromSeconds(30),
                };
                x.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.Response.Headers.Add("Token-Error", context.ErrorDescription);
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {

                    },
                };
            });
        }
    }
}
