using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;

namespace SMovie.WebUI.Controllers.Dashboard;

public class DashboardController : Controller
{
    
    
    public IActionResult Index()
    {
        return View();
    }
}