using Microsoft.AspNetCore.Mvc;
using SMovie.WebUI.Constants;

namespace SMovie.WebUI.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View(ConstantView.Login);
        }

        public IActionResult VerifyCode(Guid id)
        {
            return View(ConstantView.Login);
        }

    }
}
