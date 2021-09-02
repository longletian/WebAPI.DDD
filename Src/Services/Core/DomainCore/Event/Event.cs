using System;
namespace DomainBase
{
    public abstract class Event 
    {
        public Event()
        {
            Id = Guid.NewGuid();
            CreateDateTime = DateTime.UtcNow;
        }
        public Guid Id { get; private set; }
        public DateTime CreateDateTime { get; }
    }
}
