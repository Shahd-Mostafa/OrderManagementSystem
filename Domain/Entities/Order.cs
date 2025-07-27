using Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.CreditCard;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Customer Customer { get; set; }
        public Invoice Invoice { get; set; }

    }
}
