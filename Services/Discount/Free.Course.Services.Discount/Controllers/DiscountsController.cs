using Free.Course.Services.Discount.Services;
using FreeCourse.Shared.BaseController;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Free.Course.Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomBaseController
    {
        private readonly IDiscountService _discountService;
        private readonly ISharedIndetityService _sharedIndetityService;

        public DiscountsController(IDiscountService discountService, ISharedIndetityService sharedIndetityService)
        {
            _discountService = discountService;
            _sharedIndetityService = sharedIndetityService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _discountService.GetAll());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var discount = await _discountService.GetById(id);
            return CreateActionResultInstance(discount);
        }


        [HttpGet]
        [Route("/api/[controller]/[action]/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var userId = _sharedIndetityService.GetUserId;
            var discount = await _discountService.GetByCodeAndUserId(userId, code);

            return CreateActionResultInstance(discount);
        }


        [HttpPost]
        public async Task<IActionResult> Save(Model.Discount discount)
        {
            return CreateActionResultInstance(await _discountService.Save(discount));
        }


        [HttpPut]
        public async Task<IActionResult> Update(Model.Discount discount)
        {
            return CreateActionResultInstance(await _discountService.Update(discount));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResultInstance(await _discountService.Delete(id));
        }
    }
}
