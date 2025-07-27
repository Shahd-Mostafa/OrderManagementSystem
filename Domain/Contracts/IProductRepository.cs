using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<bool> IsProductStockAvailable(int productId, int quantity);
    }
}
