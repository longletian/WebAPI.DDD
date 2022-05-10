using Castle.DynamicProxy;
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
        public void Intercept(IInvocation invocation)
        {
            throw new NotImplementedException();
        }
    }
}
