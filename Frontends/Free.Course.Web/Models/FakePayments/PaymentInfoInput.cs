using Free.Course.Web.Models.Orders;

namespace Free.Course.Web.Models.FakePayments
{
    public class PaymentInfoInput
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public decimal TotalPrice { get; set; }

        public OrderCreatInput Order { get; set; }
    }
}
