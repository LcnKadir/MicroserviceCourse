using Free.Course.Services.Basket.Dtos;
using Free.Course.Services.Basket.Services;
using FreeCourse.Shared.BaseController;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Free.Course.Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketServies _basketServies;
        private readonly ISharedIndetityService _sharedIndetityService;

        public BasketsController(IBasketServies basketServies, ISharedIndetityService sharedIndetityService)
        {
            _basketServies = basketServies;
            _sharedIndetityService = sharedIndetityService;
        }


        [HttpGet]
        public async Task<IActionResult> GetBasket() 
        {
            return CreateActionResultInstance(await _basketServies.GetBasket(_sharedIndetityService.GetUserId));
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
        {

            basketDto.UserId = _sharedIndetityService.GetUserId;

            var response = await _basketServies.SaveOrUpdate(basketDto);

            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
        {
            return CreateActionResultInstance(await _basketServies.Delete(_sharedIndetityService.GetUserId));
        }
    }
}
