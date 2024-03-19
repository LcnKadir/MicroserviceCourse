using Free.Course.Web.Models.Ordes;

namespace Free.Course.Web.Services.Interfaces
{
    public interface IOrderService
    {
        //Synchronous Communication-direct request to Order Microservice. //Senkron İletişim-direkt Order Microservice'ne istek yapılacak.
        Task<OrderCreatedViewModel> CreateOrder(ChechoutInfoInput chechoutInfoInput);

        //Asynchronous Communication-order information will be sent to rabbitMQ. //Asenkron İletişim-sipariş bilgileri rabbitMQ'ya gönderilecek.
        Task SuspendOrder(ChechoutInfoInput chechoutInfoInput);

        Task<List<OrderViewModel>> GetOrder();
    }
}
