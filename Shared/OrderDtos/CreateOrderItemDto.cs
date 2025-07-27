using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderDtos
{
    public record CreateOrderItemDto
    {
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        //public decimal UnitPrice { get; set; }
        //public decimal Discount { get; set; } = 0;
    }
}
