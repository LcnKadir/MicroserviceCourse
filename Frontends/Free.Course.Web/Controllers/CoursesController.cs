using Free.Course.Web.Services.Interfaces;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
