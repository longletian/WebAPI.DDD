using DomainBase;

namespace Ordering.Domain
{
    public class Buyer : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
    }
}
