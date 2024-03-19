using Free.Course.Services.FakePayment.Models;
using FreeCourse.Shared.BaseController;
using FreeCourse.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Free.Course.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {

        [HttpPost]
        public IActionResult ReceivePayment(PaymentDto paymentDto)
        {
            //Make a payment with PaymentDto. //PaymentDto ile ödeme işlemi gerçekleştir.

            return CreateActionResultInstance(Response<NoContent>.Success(200));

        }
    }
}
