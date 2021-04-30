using DomainBase.Event;
using System.Threading.Tasks;

namespace EventBusBase
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TIntegrationEvent"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        Task PublishAsync<TIntegrationEvent>(TIntegrationEvent @event)
           where TIntegrationEvent : Event;
    }
}
