using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ProductDtos
{
    public record ProductResultDto(int productId, string Name, double price,double Stock );
}