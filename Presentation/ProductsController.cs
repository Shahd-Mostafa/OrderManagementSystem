using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.CustomerDtos;
using Shared.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class ProductsController:ApiController
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{productId}")]

        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _productService.GetByIdAsync(productId);
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDto dto)
        {
            var product = await _productService.AddAsync(dto);
            return Ok(product);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProductDto dto)
        {
            var product = await _productService.UpdateAsync(productId, dto);
            return Ok(product);
        }
    }
}
