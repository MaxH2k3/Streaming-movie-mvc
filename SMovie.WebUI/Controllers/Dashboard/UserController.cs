using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;

namespace SMovie.WebUI.Controllers.Dashboard;

public class UserController : Controller
{
    public readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    public IActionResult Index()
    {
        return View();
    }
}