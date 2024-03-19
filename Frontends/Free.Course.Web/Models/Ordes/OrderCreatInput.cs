namespace Free.Course.Web.Models.Ordes
{
    public class OrderCreatInput
    {
        public OrderCreatInput()
        {
            OrderItems = new List<OrderItemCreatInput>();
        }


        public string BuyerId { get; set; }

        public List<OrderItemCreatInput> OrderItems { get; set; }

        public AddressCreatInput Address { get; set; }
    }
}
