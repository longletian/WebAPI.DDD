using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Units { get; set; }
        public string PictureFileName { get; set; }

        public OrderItem()
        {
            OrderId = Guid.Empty;
            ProductName = string.Empty;
            PictureFileName = string.Empty;
        }
    }
}
