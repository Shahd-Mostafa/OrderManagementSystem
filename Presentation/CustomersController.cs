using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.CustomerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [Authorize]
    public class CustomersController: ApiController
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        public CustomersController(ICustomerService customerService, IOrderService orderService)
        {
            _customerService = customerService;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto dto)
        {
            var customer= await _customerService.AddCustomerAsync(dto);
            return Ok(customer);
        }

        [HttpGet("{customerId}/orders")]
        public async Task<IActionResult> GetCustomersOrders(int customerId)
        {
            var orders= await _orderService.GetOrderByCustomerAsync(customerId);
            return Ok(orders);
        }
    }
}
