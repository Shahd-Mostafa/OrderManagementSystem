using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstraction;
using Shared.CustomerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CustomerResultDto> AddCustomerAsync(CreateCustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            await _unitOfWork.CustomersRepository.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();
            return new CustomerResultDto
            {
                customerId = customer.CustomerId,
                Name=customer.Name,
                Email=customer.Email
            };
        }
    }
}
