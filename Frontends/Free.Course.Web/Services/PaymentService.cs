using Free.Course.Web.Models.FakePayments;
using Free.Course.Web.Services.Interfaces;

namespace Free.Course.Web.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
        {
            throw new NotImplementedException();
        }
    }
}
