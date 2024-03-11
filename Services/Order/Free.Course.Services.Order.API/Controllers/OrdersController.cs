using Free.Course.Services.Order.Application.Commands;
using Free.Course.Services.Order.Application.Queries;
using FreeCourse.Shared.BaseController;
using FreeCourse.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Free.Course.Services.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        private readonly IMediator _mediator;
        private readonly ISharedIndetityService _sharedIndetityService;

        public OrdersController(IMediator mediator, ISharedIndetityService sharedIndetityService)
        {
            _mediator = mediator;
            _sharedIndetityService = sharedIndetityService;
        }


        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var response = await _mediator.Send(new GetOrdersByUserIdQuery { UserId = _sharedIndetityService.GetUserId });

            return CreateActionResultInstance(response);
        }


        [HttpPost]
        public async Task<IActionResult> SaveOrder(CreateOrderCommand createOrderCommand)
        {
            var response = await _mediator.Send(createOrderCommand);

            return CreateActionResultInstance(response);
        }
    }
}
