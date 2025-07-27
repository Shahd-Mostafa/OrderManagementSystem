using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderDtos
{
    public record OrderResultDto(int OrderId,int CustomerId, DateTime OrderDate,double TotalAmount,string PaymentMethod,string Status, IEnumerable<OrderItemDto> OrderItems);
}
