using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainBase
{

    /// <summary>
    /// 日志中间件
    /// 中间件就是
    /// </summary>
    public class LogMiddleware : IMiddleware
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {

                throw;
            }


            throw new NotImplementedException();
        }


    }

    public class LogMiddleware1
    {
        private readonly RequestDelegate next;
        public LogMiddleware1(RequestDelegate _next)
        {
            next=_next;
        }

        //public abstract Task InvokeAsync(HttpContext context, RequestDelegate next)
        //{ 
        
        //}
    }
}
