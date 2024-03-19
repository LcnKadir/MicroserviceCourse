using Free.Course.Web.Models.FakePayments;
using Free.Course.Web.Services.Interfaces;

namespace Free.Course.Web.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
        {
            var response = await _httpClient.PostAsJsonAsync("fakepayments", paymentInfoInput);

            return response.IsSuccessStatusCode;
        }
    }
}
