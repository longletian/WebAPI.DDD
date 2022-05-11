using Ordering.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        public Task<Order> GetOrderByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
