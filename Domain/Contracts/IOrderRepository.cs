using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IOrderRepository:IRepository<Order>
    {
        //Task<IEnumerable<Order>> GetAllOrdersAsync();
        //Task<Order> GetOrderDetails(int orderId);
        Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId);
    }
}
