using Microsoft.AspNetCore.Mvc;

namespace SMovie.WebUI.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VerifyCode(Guid id)
        {
            return View();
        }

    }
}
