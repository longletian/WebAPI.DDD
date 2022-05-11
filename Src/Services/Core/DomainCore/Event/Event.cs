using System;
namespace DomainBase
{
    public record  Event 
    {
        public Guid Id { get; private set; }
        public DateTime CreateDateTime { get; }

        public Event()
        {
            Id = Guid.NewGuid();
            CreateDateTime = DateTime.UtcNow;
        }
    }
}
