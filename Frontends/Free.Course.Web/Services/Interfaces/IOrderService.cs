using Free.Course.Web.Models.Orders;

namespace Free.Course.Web.Services.Interfaces
{
    public interface IOrderService
    {
        //Synchronous Communication-direct request to Order Microservice. //Senkron İletişim-direkt Order Microservice'ne istek yapılacak.
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput);

        //Asynchronous Communication-order information will be sent to rabbitMQ. //Asenkron İletişim-sipariş bilgileri rabbitMQ'ya gönderilecek.
        Task SuspendOrder(CheckoutInfoInput checkoutInfoInput);

        Task<List<OrderViewModel>> GetOrder();
    }
}
