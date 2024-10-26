using AutoMapper;
using SalesManagement.Application.Dto;
using SalesManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Application.Common
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<CreateOrderDto, Order>().ReverseMap();

            CreateMap<CreateProductOrderDto,ProductOrder>().ReverseMap();
            CreateMap<ProductOrderDto,ProductOrder>().ReverseMap(); 
        }
    }
}
