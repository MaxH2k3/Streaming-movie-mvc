using Microsoft.AspNetCore.Mvc;

namespace SMovie.WebUI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
