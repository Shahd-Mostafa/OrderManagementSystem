using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderManagementDbContext _context;
        public OrderRepository(OrderManagementDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Order item)
        {
            await _context.Orders.AddAsync(item);
        }

        public void DeleteAsync(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
                _context.Orders.Remove(order);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.Include(o => o.Customer)
                        .Include(o => o.OrderItems)
                            .ThenInclude(oi => oi.Product)
                        .Include(o => o.Invoice)
                        .ToListAsync(); ;
        }

        public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Orders.Where(o=>o.CustomerId == customerId).Include(o=>o.OrderItems).ThenInclude(oi => oi.Product)
                .Include(o => o.Invoice).ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int orderId)
        {
            return await _context.Orders.Include(o => o.Customer)
                        .Include(o => o.OrderItems)
                            .ThenInclude(oi => oi.Product)
                        .Include(o => o.Invoice)
                        .FirstOrDefaultAsync(o => o.OrderId == orderId); ;
        }
        public void UpdateAsync(Order item)
        {
            _context.Orders.Update(item);
        }
    }
}
