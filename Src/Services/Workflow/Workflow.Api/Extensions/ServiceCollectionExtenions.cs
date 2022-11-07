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
using InfrastructureBase;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using InfrastructureBase.Data;
using System.Reflection;
using System.Collections.Generic;
using System.Text.Json;
using InfrastructureBase.EventBus;
using RabbitMQ.Client;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using DomainBase;
using InfrastructureBase.Base.AuthBase.CustomAuth;
using System.Threading;
using Elsa;
using Elsa.Activities.UserTask.Extensions;
using Elsa.Persistence.EntityFramework.Core;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.MySql;
using Elsa.Providers.Workflows;
using Elsa.Runtime;
using InfrastructureBase.Data.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Storage.Net;
using Workflow.Api.Infrastructure.Data.StartupTasks;
using Workflow.Api.Infrastructure.Workflow.Activities;
using Workflow.Api.Workflow.Providers;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using MySqlElsaContextFactory = Elsa.Persistence.EntityFramework.MySql.MySqlElsaContextFactory;
using Microsoft.AspNetCore.Builder;
using Workflow.Api.Workflow.Activities;

namespace Workflow.Api
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
                .UseMonitorCommand(cmd => { Log.Information(cmd.CommandText + ";"); })
                //自动同步实体结构到数据库
                .UseAutoSyncStructure(false)
                .Build(); //请务必定义成 Singleton 单例模式

            services.AddSingleton(freeSql);
            services.AddFreeRepository();

            try
            {
                using var objPool = freeSql.Ado.MasterPool.Get();
                freeSql.Aop.CurdAfter += (s, e) =>
                {
                    Console.WriteLine($"ManagedThreadId:{Thread.CurrentThread.ManagedThreadId};" +
                                      $" FullName:{e.EntityType.FullName} ElapsedMilliseconds:{e.ElapsedMilliseconds}ms, {e.Sql}");
                    if (e.ElapsedMilliseconds > 200)
                    {
                        Log.Warning(e.Sql + ";");
                    }
                };
            }
            catch (Exception e)
            {
                Log.Error(e + e.StackTrace + e.Message + e.InnerException);
                return;
            }

            //在运行时直接生成表结构
            // try
            // {
            //     freeSql.CodeFirst
            //         .SeedData()
            //         .SyncStructure(FreeSqlExtension.GetTypesByNameSpace());
            // }
            // catch (Exception e)
            // {
            //     Log.Logger.Error(e + e.StackTrace + e.Message + e.InnerException);
            // }
        }

        /// <summary>
        /// 注入跨域
        /// </summary>
        /// <param name="services"></param>
        public static void AddCorsService(this IServiceCollection services)
        {
            services.AddCors(o => o.AddDefaultPolicy(builder =>
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
            services
                .AddHttpContextAccessor()
                .AddCustomDaprClient()
                .AddControllers((c) =>
                {
                    //c.Filters.Add();
                })
                .AddJsonOptions(option => option.JsonSerializerOptions.PropertyNamingPolicy = null)
                .AddFluentValidation(fv =>
                {
                    //是否同时支持两种验证方式
                    //fv.RunDefaultMvcValidationAfterFluentValidationExecutes = true;
                    //自定义IValidator验证空接口
                    fv.RegisterValidatorsFromAssemblyContaining<IValidator>();
                })
                .AddDapr();

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

                //以下是设置SwaggerUI中所有Web Api 的参数注释、返回值等信息从哪里获取。
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath, true);
                // 引入Swashbuckle和FluentValidation
                c.AddFluentValidationRules();


                // Jwt Bearer 认证，必须是 oauth2
                //c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                //{
                //    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                //    Name = "Authorization",//jwt默认的参数名称
                //    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                //    Type = SecuritySchemeType.ApiKey
                //});

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });
            });
        }

        /// <summary>
        /// 公共服务配置
        /// </summary>
        /// <param name="services"></param>
        public static void AddCommonService(this IServiceCollection services)
        {
            services.Configure<JwtConfig>(AppSettingConfig.GetSection("JwtConfig"));
            services.AddSingleton<IEventBus, DaprEventBus>();
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

                //配置授权
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();

                    policy.RequireClaim("scope", "api1");
                });
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
                    var parameters = new TokenValidationParameters
                    {
                        SaveSigninToken = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(30),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = AppSettingConfig.GetSection("JwtConfig:Issuer").Value.ToString(), //发行人
                        ValidAudience = AppSettingConfig.GetSection("JwtConfig:Audience").Value.ToString(), //订阅人
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingConfig
                            .GetSection("JwtConfig:IssuerSigningKey").Value.ToString())),
                    };

                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = parameters;
                    x.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/hubs/messagehub")))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        },

                        //在Token验证通过后调用
                        OnAuthenticationFailed = context =>
                        {
                            var jwtHandler = new JwtSecurityTokenHandler();
                            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                            if (!string.IsNullOrEmpty(token) && jwtHandler.CanReadToken(token))
                            {
                                var jwtToken = jwtHandler.ReadJwtToken(token);

                                if (jwtToken.Issuer != parameters.ValidIssuer)
                                {
                                    context.Response.Headers.Add("Token-Error-Iss", "issuer is wrong!");
                                }

                                if (jwtToken.Audiences.FirstOrDefault() != parameters.ValidAudience)
                                {
                                    context.Response.Headers.Add("Token-Error-Aud", "Audience is wrong!");
                                }
                            }

                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }

                            return Task.CompletedTask;
                        },

                        //未授权时调用失败
                        OnChallenge = context =>
                        {
                            if (context.Error != null)
                            {
                                string message = context.ErrorDescription;
                            }

                            context.Response.Headers.Add("Token-Error-Iss", "请授权");
                            return Task.CompletedTask;
                        },
                        //在Token验证通过后调用
                        //OnTokenValidated = context => { 
                        //}
                    };
                })
                .AddCustomAuthentication((option) => { });
        }

        /// <summary>
        /// 健康服务检查
        /// </summary>
        /// <param name="services"></param>
        public static void AddHealthCheckService(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddMySql(AppSettingConfig.GetConnStrings("MysqlCon").ToString(), "mysql-Check");
        }

        /// <summary>
        /// IDServe4授权方式
        /// </summary>
        /// <param name="services"></param>
        public static void AddIdServeAuthenticationService(this IServiceCollection services)
        {
        }

        /// <summary>
        /// 添加dapr配置服务
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddCustomDaprClient(this IServiceCollection services)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
            };

            services.AddSingleton(options);

            services.AddDaprClient(client => { client.UseJsonSerializationOptions(options); });

            return services;
        }

        /// <summary>
        /// 添加rabbitmq服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddRabbitmqService(this IServiceCollection services)
        {
            services.Configure<RabbitmqOptions>(AppSettingConfig.GetSection("RabbitMQConfig"));
            RabbitmqOptions option = services.BuildServiceProvider().GetService<IOptionsMonitor<RabbitmqOptions>>()
                .CurrentValue;
            services.AddSingleton<IRabbitmqConnection>((c) =>
            {
                // 创建连接工厂
                var factory = new ConnectionFactory()
                {
                    HostName = option.HostName,
                    UserName = option.UserName,
                    Password = option.Password,
                    Port = option.Port,
                    VirtualHost = option.VirtualHost,
                };
                return new RabbitmqConnection(factory, option.EventBusRetryCount);
            });
            services.AddSingleton<IEventBus, RabbitmqEventBus>();
            //services.AddTransient<IEventHandle<UserEvent>, UserEventHandle>();

            #region 一个接口多个实现

            services.AddSingleton(serviceProvider =>
            {
                Func<Type, IEventBus> func = key =>
                {
                    if (key != null)
                    {
                        return serviceProvider.GetServices<IEventBus>().FirstOrDefault((c) => c.GetType() == key);
                    }
                    else
                        throw new ArgumentException("");
                };
                return func;
            });

            #endregion
        }

        /// <summary>
        /// 添加工作流服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddWorkflowCoreService(this IServiceCollection services)
        {
            string connectStr = AppSettingConfig.GetConnStrings("MysqlCon").ToString();
            //services.AddWorkflow(x => x.UseMySQL(connectStr, true, true));

            //IServiceProvider serviceProvider = services.BuildServiceProvider();
            //var host = serviceProvider.GetServices<IWorkflowHost>();
            //host.ForEach((c) =>
            //{
            //    c.RegisterWorkflow<IWorkflow>();
            //});
            //services.AddTransient<StepBody>();
        }

        /// <summary>
        /// 新增Elsa工作流服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="env"></param>
        public static IServiceCollection AddWorkflowCoreElsaService(this IServiceCollection services,
            IHostEnvironment env)
        {
            var elsaSection = AppSettingConfig.GetSection("Elsa");
            var connectionString = AppSettingConfig.GetConnStrings("MysqlWorkCon");


            services.AddElsa(option => option
            // 从数据库中读取数据流 (自定义连接)
            // 最好按照官方文档,不然mysql会有迁移失败的问题
            // https://github.com/elsa-workflows/elsa-core/blob/master/src/persistence/Elsa.Persistence.EntityFramework/Elsa.Persistence.EntityFramework.MySql/DbContextOptionsBuilderExtensions.cs
              .UseEntityFrameworkPersistence(ef => ef.UseMySql(connectionString), true)

                //.UseEntityFrameworkPersistence(
                //        contextOptions => contextOptions.UseMySql(connectionString,
                //            ServerVersion.AutoDetect(connectionString), db => db
                //                .MigrationsAssembly(typeof(MySqlElsaContextFactory).Assembly.GetName().Name)
                //                .MigrationsHistoryTable(ElsaContext.MigrationsHistoryTable, ElsaContext.ElsaSchema)
                //                .SchemaBehavior(MySqlSchemaBehavior.Ignore)),true)
                .AddConsoleActivities()
                .AddHttpActivities((option) => elsaSection.GetSection("Server").Bind(option))
                .AddEmailActivities(elsaSection.GetSection("Smtp").Bind)
                // 新增自定义工作流
                // .AddWorkflow<HeartbeatWorkflow>()
                .AddJavaScriptActivities()
                .AddUserTaskActivities()
                .AddQuartzTemporalActivities()
                // 新增自定义操作,比如说钉钉消息发送
                .AddActivity<SendDingActivtity>()
                .AddActivity<FileUploadActivtity>()
                .AddWorkflowsFrom<Program>()
            );

            // Register all Mediatr event handlers from this assembly.
            services.AddNotificationHandlersFrom<Startup>();

            services.AddTransient<IHttpService, HttpService>();
            
            // services.AddWorkflowContextProvider<CaseWorkflowContextProvider>()
            services.AddStartupTask<RunWorkMigrations>();

            // services.AddHostedService<RunWorkMigrationsV1>();

            // Register custom type definition provider for JS intellisense.
            // services.AddJavaScriptTypeDefinitionProvider<CustomTypeDefinitionProvider>();

            //通过 Storage.NET 提供的抽象从 JSON 文件中读取工作流（注意顺序（需要先注入elsa服务））
            var currentAssemblyPath = Path.Combine(env.ContentRootPath, "JsonConfig");
            services.Configure<BlobStorageWorkflowProviderOptions>(options =>
                options.BlobStorageFactory = () =>
                    StorageFactory.Blobs.DirectoryFiles(Path.Combine(currentAssemblyPath, "Workflows")));

            services.AddBookmarkProvidersFrom<Program>();
            // Elsa API endpoints.
            services.AddElsaApiEndpoints()
                .AddElsaSwagger();

            services.AddRazorPages();
            
            // services.AddServerSideBlazor(); // needed for notifications

            return services;
        }

        /// <summary>
        /// 新增自定义工作流程
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddCustomWorkflowActivitiesService(this IServiceCollection services)
        {
            return services;
        }
    }
}