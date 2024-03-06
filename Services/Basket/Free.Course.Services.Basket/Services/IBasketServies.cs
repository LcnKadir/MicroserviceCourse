using Free.Course.Services.Basket.Dtos;
using FreeCourse.Shared.DTOs;

namespace Free.Course.Services.Basket.Services
{
    public interface IBasketServies
    {
        Task<Response<BasketDto>> GetBasket(string userId);

        Task<Response<bool>> SaveOrUpdate(BasketDto basket);

        Task<Response<bool>> Delete(string userId);
    }
}
