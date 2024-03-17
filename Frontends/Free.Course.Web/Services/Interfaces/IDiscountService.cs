using Free.Course.Web.Models.Discount;

namespace Free.Course.Web.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetDiscount(string discountCode);
    }
}
