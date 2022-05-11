using DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.IntegrationEvents
{
    public record OrderEvent(int OrderNumber, DateTime OrderDate, string OrderStatus) : Event;
}
