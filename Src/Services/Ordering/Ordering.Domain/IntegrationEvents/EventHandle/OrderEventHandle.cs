using DomainBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.IntegrationEvents
{
    public class OrderEventHandle : IEventHandle<OrderEvent>
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IOrderRepository orderRepository;

        public OrderEventHandle(IServiceProvider _serviceProvider, IOrderRepository _orderRepository)
        {
            serviceProvider=_serviceProvider;
            orderRepository=_orderRepository;
        }
        public Task HandleAsync(OrderEvent @event)
        {
            try
            {
                orderRepository.GetOrderByIdAsync(@event.Id);
                using (serviceProvider.CreateScope())
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
            return Task.CompletedTask;
           
        }
    }
}
