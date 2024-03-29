using Microsoft.AspNetCore.Mvc;

namespace SMovie.WebUI.Controllers.Dashboard;

public class UserController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}