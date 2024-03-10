using AutoMapper;
using Free.Course.Services.Order.Application.Dtos;
using Free.Course.Services.Order.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free.Course.Services.Order.Application.Mapping
{
     class CustomMapping: Profile
    {
        public CustomMapping() {
            CreateMap<Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        
        }
    }
}
