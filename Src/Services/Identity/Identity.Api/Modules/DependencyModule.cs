
using Autofac;
using InfrastructureBase;
using System;
using System.Linq;
using System.Reflection;

namespace Identity.Api
{
    public  class DependencyModule : Autofac.Module 
    {
        protected override void Load(ContainerBuilder builder)
        {
            //获取当前项目的程序集
            Assembly[] currentAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(r => r.FullName.StartsWith($"Identity.")|| r.FullName.Contains("Base")).ToArray();

            //每次调用，都会重新实例化对象，每次请求都创建一个对象
            Type transientDependency = typeof(ITransientDependency);
            builder.RegisterAssemblyTypes(currentAssemblies)
                .Where(t => transientDependency.GetTypeInfo().IsAssignableFrom(t) && t.IsClass && !t.IsAbstract && !t.IsGenericType)
                .AsImplementedInterfaces().InstancePerDependency();

            //同一lifetime生成的对象是同一个实例
            Type scopeDependency = typeof(IScopedDependency);
            builder.RegisterAssemblyTypes(currentAssemblies)
                .Where(t => scopeDependency.GetTypeInfo().IsAssignableFrom(t) && t.IsClass && !t.IsAbstract && !t.IsGenericType)
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            //单例模式：每次调用都会使用同一实例化的对象，每次都用同一个对象
            Type singletonDependency = typeof(ISingletonDependency);
            builder.RegisterAssemblyTypes(currentAssemblies)
                .Where(t => singletonDependency.GetTypeInfo().IsAssignableFrom(t) && t.IsClass && !t.IsAbstract && !t.IsGenericType)
                .AsImplementedInterfaces().SingleInstance();
        }
    }
}
