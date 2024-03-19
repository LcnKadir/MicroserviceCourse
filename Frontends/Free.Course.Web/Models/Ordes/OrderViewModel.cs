namespace Free.Course.Web.Models.Ordes
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string BuyerId { get; set; }
        
        public List<OrderItemViewModel> OrderItems;

    }
}
