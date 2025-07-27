using Shared.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IOrderService
    {
        Task<OrderResultDto> CreateOrderAsync(CreateOrderDto dto);
        Task<OrderResultDto> GetOrderById(int orderId);
        Task<IEnumerable<OrderResultDto>> GetAllOrdersAsync();
        Task<IEnumerable<OrderResultDto>> GetOrderByCustomerAsync(int customerId);
        Task<UpdateOrderStatusDto> UpdateOrderStatusAsync(int orderId, UpdateOrderStatusDto dto);

    }
}
