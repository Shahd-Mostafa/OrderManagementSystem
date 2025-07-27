using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderDtos
{
    public record OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        //public double Discount { get; set; }
    }
}
