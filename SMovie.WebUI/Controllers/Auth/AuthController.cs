using Microsoft.AspNetCore.Mvc;

namespace SMovie.WebUI.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View("Login");
        }

        public IActionResult VerifyCode(Guid id)
        {
            Console.WriteLine(id);
            return View("Login");
        }

    }
}
