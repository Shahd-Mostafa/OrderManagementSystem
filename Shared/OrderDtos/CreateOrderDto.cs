using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderDtos
{
    public record CreateOrderDto
    {
        public int CustomerId { get; set; }

        [Required]
        public string PaymentMethod { get; set; }
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}
