using Free.Course.Services.Basket.Dtos;
using FreeCourse.Shared.DTOs;
using System.Text.Json;

namespace Free.Course.Services.Basket.Services
{
    public class BasketService : IBasketServies
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<bool>> Delete(string userId)
        {
            var status = await _redisService.GetDb().KeyDeleteAsync(userId);

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket not found", 404);
        }

        public async Task<Response<BasketDto>> GetBasket(string userId)
        {
            var exitbasket = await _redisService.GetDb().StringGetAsync(userId);

            if (String.IsNullOrEmpty(exitbasket))
            {
                return Response<BasketDto>.Fail("Basket not found", 404);
            }

            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(exitbasket), 200);
        }

        public async Task<Response<bool>> SaveOrUpdate(BasketDto basketdto)
        {
            var status = await _redisService.GetDb().StringSetAsync(basketdto.UserId, JsonSerializer.Serialize(basketdto));

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket could not update or save", 500);
        }
    }
}
