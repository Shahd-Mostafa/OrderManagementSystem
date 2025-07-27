using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.CustomerDtos
{
    public record CustomerResultDto
    {
        public int customerId { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
    }
}
