using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ProductNotFoundException:Exception
    {
        public ProductNotFoundException(int id) : base($"Product with id:{id} was not found")
        {
        }
    }
}
