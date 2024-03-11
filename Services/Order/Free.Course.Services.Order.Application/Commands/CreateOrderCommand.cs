using Free.Course.Services.Order.Application.Dtos;
using FreeCourse.Shared.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free.Course.Services.Order.Application.Commands
{
    public class CreateOrderCommand:IRequest<Response<CreatedOrderDto>>
    {
        public string BuyerId { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }

        public AddressDto AddressDto { get; set; }
    }
}
