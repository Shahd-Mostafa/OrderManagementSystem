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
    public class ProductRepository : IProductRepository
    {
        private readonly OrderManagementDbContext _context;
        public ProductRepository(OrderManagementDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Product item)
        {
            await _context.Products.AddAsync(item);
        }

        public void DeleteAsync(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
                _context.Products.Remove(product);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<bool> IsProductStockAvailable(int productId, int quantity)
        {
            var product=await _context.Products.FindAsync(productId);
            return product != null && product.Stock >= quantity;
        }

        public void UpdateAsync(Product item)
        {
            _context.Products.Update(item);
        }
    }
}
