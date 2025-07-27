using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderDtos
{
    public record InvoiceDto
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }=DateTime.Now;
        public double TotalAmount { get; set; }
    }
}
