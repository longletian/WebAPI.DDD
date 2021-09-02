using DomainBase;
using System.Threading.Tasks;

namespace InfrastructureBase
{
    public interface IEventBus
    {
        Task PublishAsync<TIntegrationEvent>(TIntegrationEvent @event)
           where TIntegrationEvent : Event;
    }
}
