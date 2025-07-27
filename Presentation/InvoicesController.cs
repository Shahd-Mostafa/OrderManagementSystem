using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class InvoicesController : ApiController
    {
        private readonly IInvoiceService _invoiceService;
        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{invoiceId}")]
        public async Task<IActionResult> GetInvoiceById(int invoiceId)
        {
            var invoice = await _invoiceService.GetByInvoiceIdAsync(invoiceId);
            return Ok(invoice);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices = await _invoiceService.GetAllInvoicesAsync();
            return Ok(invoices);
        }

    }
}
