using DomainBase.Event;
using System.Threading.Tasks;

namespace EventBusBase
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventBus
    {
        Task Publish<T>(T @event)
            where T : Event;

        void Subscribe<T>(IEventHandler<T> handler)
            where T : Event;

    }
}
