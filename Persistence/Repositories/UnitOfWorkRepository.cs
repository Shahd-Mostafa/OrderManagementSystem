using Domain.Contracts;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWork
    {
        private readonly OrderManagementDbContext _context;
        private readonly ICustomerRepository _customers;
        private readonly IOrderRepository _orders;
        private readonly IProductRepository _products;
        private readonly IInvoiceRepository _invoices;
        public UnitOfWorkRepository(
        OrderManagementDbContext context,
        ICustomerRepository customers,
        IOrderRepository orders,
        IProductRepository products,
        IInvoiceRepository invoices)
            {
            _context = context;
            _customers = customers;
            _orders = orders;
            _products = products;
            _invoices = invoices;
        }
        public ICustomerRepository CustomersRepository => _customers;

        public IOrderRepository OrdersRepository =>_orders;

        public IProductRepository ProductsRepository => _products;

        public IInvoiceRepository InvoicesRepository => _invoices;

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
