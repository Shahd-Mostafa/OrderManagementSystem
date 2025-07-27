using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ProductDtos
{
    public record CreateProductDto
    {
        [Required]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue)]
        public double Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
    }
}
