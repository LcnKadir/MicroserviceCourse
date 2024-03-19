using Free.Course.Web.Models.FakePayments;
using Free.Course.Web.Models.Ordes;
using Free.Course.Web.Services.Interfaces;
using FreeCourse.Shared.DTOs;
using FreeCourse.Shared.Services;

namespace Free.Course.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPaymentService _paymentService;
        private readonly HttpClient _httpClient;
        private readonly IBasketService _basketService;
        private readonly ISharedIndetityService _sharedIndetityService;
        public OrderService(IPaymentService paymentService, HttpClient httpClient, IBasketService basketService, ISharedIndetityService sharedIndetityService)
        {
            _paymentService = paymentService;
            _httpClient = httpClient;
            _basketService = basketService;
            _sharedIndetityService = sharedIndetityService;
        }

        public async Task<OrderCreatedViewModel> CreateOrder(ChechoutInfoInput chechoutInfoInput)
        {
            var basket = await _basketService.Get();

            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = chechoutInfoInput.CardName,
                CardNumber = chechoutInfoInput.CardNumber,
                Expiration = chechoutInfoInput.Expiration,
                CVV = chechoutInfoInput.CVV,
                TotalPrice = basket.TotalPrice
            };

            var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

            if (!responsePayment)
            {
                return new OrderCreatedViewModel() { Error = "Ödeme alınamadı", IsSuccessful = false };
            }

            var orderCreatInput = new OrderCreatInput()
            {
                BuyerId = _sharedIndetityService.GetUserId,
                Address = new AddressCreatInput() { Province = chechoutInfoInput.Province, District = chechoutInfoInput.District, Street = chechoutInfoInput.Street, Line = chechoutInfoInput.Line, ZipCode = chechoutInfoInput.ZipCode },

            };

            basket.BasketItems.ForEach(x =>
            {
                var orederItem = new OrderItemCreatInput { ProductId = x.CourseId, Price = x.Price, PictureUrl = "", ProductName = x.CourseName };
                orderCreatInput.OrderItems.Add(orederItem);
            });

            var response = await _httpClient.PostAsJsonAsync<OrderCreatInput>("orders", orderCreatInput);

            if (!response.IsSuccessStatusCode)
            {
                return new OrderCreatedViewModel() { Error = "Sipariş oluşturulamadı", IsSuccessful = false };
            }

            return await response.Content.ReadFromJsonAsync<OrderCreatedViewModel>();

        }

        public async Task<List<OrderViewModel>> GetOrder()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");

            return response.Data;
        }

        public Task SuspendOrder(ChechoutInfoInput chechoutInfoInput)
        {
            throw new NotImplementedException();
        }
    }
}
