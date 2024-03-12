using Microsoft.AspNetCore.Mvc;

namespace Free.Course.Web.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }
    }
}
