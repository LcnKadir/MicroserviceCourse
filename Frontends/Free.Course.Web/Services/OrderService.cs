using Free.Course.Web.Models.FakePayments;
using Free.Course.Web.Models.Orders;
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

        public async Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();

            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                Expiration = checkoutInfoInput.Expiration,
                CVV = checkoutInfoInput.CVV,
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
                Address = new AddressCreatInput() { Province = checkoutInfoInput.Province, District = checkoutInfoInput.District, Street = checkoutInfoInput.Street, Line = checkoutInfoInput.Line, ZipCode = checkoutInfoInput.ZipCode },

            };

            basket.BasketItems.ForEach(x =>
            {
                var orederItem = new OrderItemCreatInput { ProductId = x.CourseId, Price = x.GetCurrentPrice, PictureUrl = "", ProductName = x.CourseName };
                orderCreatInput.OrderItems.Add(orederItem);
            });

            var response = await _httpClient.PostAsJsonAsync<OrderCreatInput>("orders", orderCreatInput);

            if (!response.IsSuccessStatusCode)
            {
                return new OrderCreatedViewModel() { Error = "Sipariş oluşturulamadı", IsSuccessful = false };
            }

            var orderCreatedViewModel = await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();

            orderCreatedViewModel.Data.IsSuccessful = true;
            await _basketService.Delete();
            return orderCreatedViewModel.Data;
        }

        public async Task<List<OrderViewModel>> GetOrder()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");

            return response.Data;
        }

        public async Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();
            var orderCreatInput = new OrderCreatInput()
            {
                BuyerId = _sharedIndetityService.GetUserId,
                Address = new AddressCreatInput() { Province = checkoutInfoInput.Province, District = checkoutInfoInput.District, Street = checkoutInfoInput.Street, Line = checkoutInfoInput.Line, ZipCode = checkoutInfoInput.ZipCode },

            };

            basket.BasketItems.ForEach(x =>
            {
                var orederItem = new OrderItemCreatInput { ProductId = x.CourseId, Price = x.GetCurrentPrice, PictureUrl = "", ProductName = x.CourseName };
                orderCreatInput.OrderItems.Add(orederItem);
            });

                    
            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                Expiration = checkoutInfoInput.Expiration,
                CVV = checkoutInfoInput.CVV,
                TotalPrice = basket.TotalPrice,
                Order = orderCreatInput,
            };
            var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

            if (!responsePayment)
            {
                return new OrderSuspendViewModel() { Error = "Ödeme alınamadı", IsSuccessful = false };
            }
            return new OrderSuspendViewModel() { IsSuccessful = true };

        }
    }
}
