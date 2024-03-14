using Free.Course.Web.Services.Interfaces;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Free.Course.Web.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIndetityService _sharedIndetityService;

        public CoursesController(ICatalogService catalogService, ISharedIndetityService sharedIndetityService)
        {
            _catalogService = catalogService;
            _sharedIndetityService = sharedIndetityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCourseByUserIdAsync(_sharedIndetityService.GetUserId));
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategoryAsync();

            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            return View();
        }
    }
}
