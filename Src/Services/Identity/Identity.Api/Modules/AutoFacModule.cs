using Autofac;
using System.Reflection;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Identity.Api
{
    public class AutoFacModule : Autofac.Module
    {
        /// <summary>
        /// 重写autofac管道load方法
        /// </summary>
        protected override void Load(ContainerBuilder builder)
        {
            #region 注入方式
            //1：类型注入
            //builder.RegisterType<>().As<>();
            //2：实例注入
            //var output = new StringWriter();
            //builder.RegisterInstance(output).As<TextWriter>();
            //3：对象注入
            //builder.Register(c => new ConfigReader("mysection")).As<IConfigReader>();
            //4：泛型注入
            //builder.RegisterGeneric(typeof(NHibernateRepository<>)) .As(typeof(IRepository<>)).InstancePerLifetimeScope();
            //5：程序集注入

            //注册拦截器
            //builder.RegisterType<ValidatorAop>();
            //对目标类型启动动态代理，并注入自定义拦截器拦截
            //builder.RegisterAssemblyTypes(GetAssemblyByName("WebApi.Models"))
            //        .Where(a => a.Name.EndsWith("Validator"))
            //        .AsImplementedInterfaces()
            //      .InstancePerDependency()
            //      .EnableInterfaceInterceptors().InterceptedBy(typeof(ValidatorAop));
            #endregion

            #region 注入
            var basePath = AppContext.BaseDirectory;

            var cacheType = new List<Type>();
            //builder.RegisterType<LogAop>();
            //cacheType.Add(typeof(LogAop));

            builder.RegisterAssemblyTypes(GetAssemblies("Identity."))
                .Where(a => a.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerDependency()
                //
                .PropertiesAutowired()
                //引用Autofac.Extras.DynamicProxy;
                .EnableClassInterceptors()
                //允许将拦截器服务的列表分配给注册。
                .InterceptedBy(cacheType.ToArray());

            var servicesDllFile = Path.Combine(basePath, "Identity.Application.dll");
            var servicesDllFiles = Assembly.LoadFrom(servicesDllFile);

            builder.RegisterAssemblyTypes(servicesDllFiles)
             .Where(a => a.Name.Contains("Service"))
             .AsImplementedInterfaces()
             .PropertiesAutowired()
             .InstancePerDependency();
            #endregion

        }

        public Assembly[] GetAssemblies(string assemblyName)
        {
            //无法找到Identity.Application的程序集
            //var assemblyTypes = AppDomain.CurrentDomain.GetAssemblies().Where((c) => c.FullName.StartsWith($"{assemblyName}")).ToArray();
            return AppDomain.CurrentDomain.GetAssemblies().Where((c) => c.FullName.StartsWith($"{assemblyName}")).ToArray();
        }
    }
}
