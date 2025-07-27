using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.CustomerDtos
{
    public record CreateCustomerDto(string Name,string Email);
}
