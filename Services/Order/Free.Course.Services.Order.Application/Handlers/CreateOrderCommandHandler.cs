using Free.Course.Services.Order.Application.Commands;
using Free.Course.Services.Order.Application.Dtos;
using Free.Course.Services.Order.Domain.OrderAggregate;
using Free.Course.Services.Order.Infrastructure;
using FreeCourse.Shared.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free.Course.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddres = new Address(request.AddressDto.Province, request.AddressDto.District, request.AddressDto.ZipCode, request.AddressDto.Line, request.AddressDto.Street);

            Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(request.BuyerId, newAddres);

            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
            });

            await _context.Orders.AddAsync(newOrder);

            await _context.SaveChangesAsync();

            return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id }, 200);
        }
    }
}
