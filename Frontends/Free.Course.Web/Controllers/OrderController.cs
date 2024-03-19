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
            var orderStatus = await _orderService.CreateOrder(checkoutInfoInput);

            if(!orderStatus.IsSuccessful)
            {
                var basket = await _basketService.Get();
                ViewBag.basket = basket;

                ViewBag.Error = orderStatus.Error;
                return View();
            }

            return RedirectToAction(nameof(SuccessFulCheckout), new { orederId= orderStatus.OrderId});
        }

        public IActionResult SuccessFulCheckout(int orderId)
        {
            ViewBag.orderId = orderId;
            return View();
        }
    }
}
