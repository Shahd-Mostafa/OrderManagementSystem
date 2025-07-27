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
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly OrderManagementDbContext _context;
        public InvoiceRepository(OrderManagementDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Invoice item)
        {
            await _context.Invoices.AddAsync(item);
        }

        public void DeleteAsync(int id)
        {
            var invoice = _context.Invoices.Find(id);
            if (invoice != null)
                _context.Invoices.Remove(invoice);
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _context.Invoices.Include(i => i.Order)
                    .ThenInclude(o => o.Customer)
                .Include(i => i.Order)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                .ToListAsync(); ;
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            return await _context.Invoices.Include(i => i.Order)
                        .ThenInclude(o => o.Customer)
                        .Include(i => i.Order)
                        .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                        .FirstOrDefaultAsync(i => i.InvoiceId == id); ;
        }

        public void UpdateAsync(Invoice item)
        {
            _context.Invoices.Update(item);
        }
    }
}
