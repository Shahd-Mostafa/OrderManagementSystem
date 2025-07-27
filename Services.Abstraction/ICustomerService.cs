using Shared.CustomerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface ICustomerService
    {
        Task<CustomerResultDto> AddCustomerAsync(CreateCustomerDto dto);
    }
}
