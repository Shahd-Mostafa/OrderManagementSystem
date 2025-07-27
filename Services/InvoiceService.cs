using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Services.Abstraction;
using Shared.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InvoiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync()
        {
            var invoices= await _unitOfWork.InvoicesRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
        }

        public async Task<InvoiceDto> GetByInvoiceIdAsync(int id)
        {
            var invoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(id);
            if (invoice == null) throw new NotFoundException("Invoice not found");
            return _mapper.Map<InvoiceDto>(invoice);
        }
    }
}
