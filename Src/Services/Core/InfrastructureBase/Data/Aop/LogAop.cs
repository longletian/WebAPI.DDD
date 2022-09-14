using Castle.DynamicProxy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureBase
{
    /// <summary>
    /// Aop动态代理日志
    /// </summary>
    public class LogAop : IInterceptor
    {
        private readonly IHttpContextAccessor accessor;
        private readonly IWebHostEnvironment environment;
        public LogAop(
            IHttpContextAccessor _accessor,
            IWebHostEnvironment _environment)
        {
            this.environment = _environment;
            this.accessor = _accessor;
        }

        public void Intercept(IInvocation invocation)
        {
            throw new NotImplementedException();
        }
    }
}
