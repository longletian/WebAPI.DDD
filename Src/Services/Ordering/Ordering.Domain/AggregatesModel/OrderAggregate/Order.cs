using DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain
{
    public class Order: Entity,IAggregateRoot
    {
        public int OrderNumber { get; private set; }
        public DateTime OrderDate { get; private set; }
        public string OrderStatus { get; set; }
        public string Description { get; set; }
        public Address Address { get; private set; }
        public string BuyerId { get; private set; }
        public string BuyerEmail { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }
    }
}
