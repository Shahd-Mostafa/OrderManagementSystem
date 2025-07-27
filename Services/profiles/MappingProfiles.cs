using AutoMapper;
using Domain.Entities;
using Shared.AuthDtos;
using Shared.CustomerDtos;
using Shared.OrderDtos;
using Shared.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.mappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateCustomerDto, Customer>().ReverseMap();
            CreateMap<CreateCustomerDto, Customer>().ReverseMap();

            CreateMap<OrderResultDto, Order>().ReverseMap();
            CreateMap<CreateOrderDto, Order>().ReverseMap();
            CreateMap<InvoiceDto, Invoice>().ReverseMap();
            CreateMap<OrderItemDto, OrderItem>().ReverseMap();
            CreateMap<UpdateOrderStatusDto, Order>().ReverseMap();

            CreateMap<CreateProductDto, Product>().ReverseMap();
            CreateMap<ProductResultDto, Product>().ReverseMap();
            CreateMap<UpdateProductDto, Product>().ReverseMap();

            CreateMap<RegisterDto, User>().ReverseMap();
            CreateMap<LoginDto, User>().ReverseMap();
            CreateMap<UserResultDto, User>().ReverseMap();
        }
    }
}
