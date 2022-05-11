using Castle.Core.Logging;
using DomainBase;
using Identity.Domain.IntegrationEvents;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Domain.IntegrationEvents
{
    public class UserEventHandle : IEventHandle<UserEvent>
    {
        private readonly ILogger logger;
        public  UserEventHandle(ILogger _logger)
        {
            logger = _logger;
        }

        public Task HandleAsync(UserEvent @event)
        {
            logger.Info($"请求方法触发{JsonConvert.SerializeObject(@event)}");
            Console.WriteLine($"请求方法触发{JsonConvert.SerializeObject(@event)}");
            //Task.FromResult()区别是否需要返回值
            return Task.CompletedTask;
        }
    }
}
