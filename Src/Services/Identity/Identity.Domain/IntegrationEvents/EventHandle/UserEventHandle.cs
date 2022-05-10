using DomainBase;
using Identity.Domain.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Domain.Events
{
    public class UserEventHandle : IEventHandle<UserEvent>
    {

        public Task Handle(UserEvent @event, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
