using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ProductDtos
{
    public record UpdateProductDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Stock { get; set; }
    }
}
