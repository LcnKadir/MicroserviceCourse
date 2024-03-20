using Free.Course.Services.FakePayment.Models;
using FreeCourse.Shared.BaseController;
using FreeCourse.Shared.DTOs;
using FreeCourse.Shared.Messages;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Free.Course.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public FakePaymentsController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
        {
            // Payment will be processed with PaymentDto. // PaymentDto ile ödeme işlemi gerçekleştirilecek.

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

            var createorderMessageCommand = new CreateOrderMessageCommand();

            createorderMessageCommand.BuyerId = paymentDto.Order.BuyerId;
            createorderMessageCommand.Province = paymentDto.Order.Address.Province;
            createorderMessageCommand.District = paymentDto.Order.Address.District;
            createorderMessageCommand.Street = paymentDto.Order.Address.Street;
            createorderMessageCommand.Line = paymentDto.Order.Address.Line;
            createorderMessageCommand.ZipCode = paymentDto.Order.Address.ZipCode;

            paymentDto.Order.OrderItems.ForEach(x =>
            {
                createorderMessageCommand.OrderItems.Add(new OrderItem
                {
                    PictureUrl = x.PictureUrl,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                });
            });

            await sendEndpoint.Send<CreateOrderMessageCommand>(createorderMessageCommand);

            return CreateActionResultInstance(FreeCourse.Shared.DTOs.Response<NoContent>.Success(200));

        }
    }
}
