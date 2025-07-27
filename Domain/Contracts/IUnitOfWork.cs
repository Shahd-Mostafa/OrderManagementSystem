
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IUnitOfWork: IDisposable
    {
        ICustomerRepository CustomersRepository { get; }
        IOrderRepository OrdersRepository { get; }
        IProductRepository ProductsRepository { get; }
        IInvoiceRepository InvoicesRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
