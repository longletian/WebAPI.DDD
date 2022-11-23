using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Workflow.Api
{
    public static class MiddlewareExtensions
    {
        /// <summary>
        ///异常中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static void UseLogMiddleware(this IApplicationBuilder app)
        {
            // 一般用于注册自定义封装的中间件，内部其实是使用Use的方式进行中间件注册；
            // 管道 框架用来封装请求的组件
            // 中间件 应用管道中一个组件，典型的aop,用来拦截请求过程中进行一些其他的处理和响应
            // 中间件可以对管道里的任何一个请求进行拦截，可以决定是否将请求转移到一个中间件
            //app.UseMiddleware<LogMiddleware>();
        }

        public static void UseMiddleware1(this IApplicationBuilder app)
        {
            //1. 通过Use的方式注册中间件，可以控制是否将请求传递到下一个中间件；
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("hello1");
            //    // 管道短路
            //    //await next.Invoke();
            //});
        }

        public static void UseMiddleware2(this IApplicationBuilder app)
        {
            //2. 通过Run的方式注册中间件，一般用于断路或请求管道末尾，即不会将请求传递下去；
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("hello2");
            });
        }


        public static void UseDefindMiddleware(this IApplicationBuilder app)
        {
            //3. 创建管道分支
            //请求管道中增加分支，条件满足之后就由分支管道进行处理，而不会切换回主管道；Map用于请求路径匹配，而MapWhen可以有更多的条件进行过滤；
            app.Map("/map1", UseMiddleware1);

            app.Map("/map2", UseMiddleware2);

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("hello END");
            });
        }
    }
}
