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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OrderManagementDbContext _context;
        public CustomerRepository(OrderManagementDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Customer item)
        {
            await _context.Customers.AddAsync(item);
        }

        public void DeleteAsync(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
                _context.Customers.Remove(customer);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersOrdersAsync(int CustomerId)
        {
            return await _context.Customers.Where(c=>c.CustomerId == CustomerId).Include(c=>c.Orders).ThenInclude(c=>c.OrderItems).ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public void UpdateAsync(Customer item)
        {
            _context.Customers.Update(item);
        }
    }
}
