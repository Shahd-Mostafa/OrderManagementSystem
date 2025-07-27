using Shared.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResultDto>> GetAllAsync();
        Task<ProductResultDto> GetByIdAsync(int id);
        Task<ProductResultDto> AddAsync(CreateProductDto dto);
        Task<ProductResultDto> UpdateAsync(int id, UpdateProductDto dto);
    }
}
