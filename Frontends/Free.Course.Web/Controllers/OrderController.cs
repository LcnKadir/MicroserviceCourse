using Free.Course.Web.Models.Orders;
using Free.Course.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Free.Course.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public OrderController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.Get();
            ViewBag.basket = basket;
            return View(new CheckoutInfoInput());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoInput checkoutInfoInput)
        {
            //1st way synchronous communication // 1. yol senkron iletişim

            //var orderStatus = await _orderService.CreateOrder(checkoutInfoInput);

            //if (!orderStatus.IsSuccessful)
            //{
            //    var basket = await _basketService.Get();
            //    ViewBag.basket = basket;

            //    ViewBag.Error = orderStatus.Error;
            //    return View();
            //}
            //return RedirectToAction(nameof(SuccessFulCheckout), new { orderId = orderStatus.OrderId });


            //2nd way asynchronous communication //2. yol asenkron iletişim
            var orderSuspend = await _orderService.SuspendOrder(checkoutInfoInput);
            if (!orderSuspend.IsSuccessful)
            {
                var basket = await _basketService.Get();
                ViewBag.basket = basket;

                ViewBag.Error = orderSuspend.Error;
                return View();
            }

            return RedirectToAction(nameof(SuccessFulCheckout), new { orderId = new Random().Next(1, 1000) });


        }

        public IActionResult SuccessFulCheckout(int orderId)
        {
            ViewBag.orderId = orderId;
            return View();
        }

        public async Task<IActionResult> CheckoutHistory()
        {
            var orders = await _orderService.GetOrder();  
            return View(orders);
        }
    }
}
