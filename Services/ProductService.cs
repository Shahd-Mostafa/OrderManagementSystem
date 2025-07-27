using AutoMapper; 
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Services.Abstraction;
using Shared.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService: IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ProductResultDto> AddAsync(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            await _unitOfWork.ProductsRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return new ProductResultDto
            (
                product.ProductId,
                product.Name,
                product.Price,
                product.Stock
            );

        }

        public async Task<IEnumerable<ProductResultDto>> GetAllAsync()
        {
            var products= await _unitOfWork.ProductsRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductResultDto>>(products);
        }

        public async Task<ProductResultDto> GetByIdAsync(int id)
        {
            var product= await _unitOfWork.ProductsRepository.GetByIdAsync(id);
            if(product ==null)
                throw new ProductNotFoundException(id);
            return _mapper.Map<ProductResultDto>(product);
        }

        public async Task<ProductResultDto> UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(id);
            if (product == null)
                throw new ProductNotFoundException(id);
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Stock =dto.Stock;

            await _unitOfWork.SaveChangesAsync();

            return new ProductResultDto
            (
                product.ProductId,
                product.Name,
                product.Price,
                product.Stock
            );

        }
    }
}
