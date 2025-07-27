using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.Enums;
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
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
        }
        public async Task<OrderResultDto> CreateOrderAsync(CreateOrderDto dto)
        {
            var customer= await _unitOfWork.CustomersRepository.GetByIdAsync(dto.CustomerId);
            if (customer == null)
            {
                throw new NotFoundException("Customer Not Found");
            }
            double total = 0;
            var orderItems = new List<OrderItem>();
            foreach(var item in dto.Items)
            {
                var product= await _unitOfWork.ProductsRepository.GetByIdAsync(item.ProductId);
                if(product == null)
                {
                    throw new NotFoundException("Product Not Found");
                }
                if(product.Stock< item.Quantity)
                {
                    throw new InvalidOperationException($"Insufficient stock for product {product.Name}.");
                }
                var unitPrice = product.Price;
                var discount = 0.0;
                var orderItem = new OrderItem
                {
                    ProductId=product.ProductId,
                    Quantity=item.Quantity,
                    UnitPrice=unitPrice,
                    Discount=discount
                };

                orderItems.Add(orderItem);
                total += (unitPrice * item.Quantity);
                product.Stock -= item.Quantity;

            }

            var discountRate = 0.0;
            if (total > 200)
            {
                discountRate = 0.1;
            }
            else if (total > 100)
            {
                discountRate = 0.05;
            }

            var totalWithDiscount = (1-discountRate) * total;
            if (!Enum.TryParse<PaymentMethod>(dto.PaymentMethod, true, out var paymentMethod))
            {
                throw new InvalidOperationException("Invalid payment method.");
            }

            var order = new Order
            {
                CustomerId = dto.CustomerId,
                OrderDate = DateTime.UtcNow,
                TotalAmount= totalWithDiscount,
                OrderItems= orderItems,
                PaymentMethod= paymentMethod,
                Status = OrderStatus.Pending,
            };
            await _unitOfWork.OrdersRepository.AddAsync(order);
            var invoice = new Invoice
            {
                Order = order,
                InvoiceDate = DateTime.UtcNow,
                TotalAmount= totalWithDiscount,
            };
            await _unitOfWork.InvoicesRepository.AddAsync(invoice);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<OrderResultDto>(order);

        }

        public async Task<IEnumerable<OrderResultDto>> GetAllOrdersAsync()
        {
            var orders = await _unitOfWork.OrdersRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderResultDto>>(orders);
        }

        public async Task<IEnumerable<OrderResultDto>> GetOrderByCustomerAsync(int customerId)
        {
            var orders = await _unitOfWork.OrdersRepository.GetByCustomerIdAsync(customerId);
            return _mapper.Map<IEnumerable<OrderResultDto>>(orders);
        }

        public async Task<OrderResultDto> GetOrderById(int orderId)
        {
            var order = await _unitOfWork.OrdersRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new NotFoundException("Order Not Found");
            return _mapper.Map<OrderResultDto>(order);
        }

        public async Task<UpdateOrderStatusDto> UpdateOrderStatusAsync(int orderId, UpdateOrderStatusDto dto)
        {
            var order = await _unitOfWork.OrdersRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new NotFoundException("Order Not Found.");
            order.Status = Enum.Parse<OrderStatus>(dto.NewStatus);
            await _unitOfWork.SaveChangesAsync();
            var customerEmail = order.Customer?.Email;
            if (!string.IsNullOrEmpty(customerEmail))
            {
                await _emailService.SendEmailAsync(
                    customerEmail,
                    $"Your Order #{order.OrderId} Status Changed",
                    $"The status of your order has been updated to: <strong>{order.Status}</strong>."
                );
            }
            return new UpdateOrderStatusDto
            (
                order.Status.ToString()
            );
        }
    }
}
