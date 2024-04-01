using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;
using SMovie.Domain.Models;
using SMovie.WebUI.Constants;
using SMovie.WebUI.Enums;
using SMovie.WebUI.Models;

namespace SMovie.WebUI.Controllers.Dashboard;

public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService,
            IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    public async Task<IActionResult> Index()
    {
        ViewData["Menu"] = (int)MenuDashboard.Users;

        var users = await _userService.GetAll();

        if(users == null)
        {
            return View(ConstantView.UserList);
        }

        var result = _mapper.Map<IEnumerable<UserDetail>>(users);

        return View(ConstantView.UserList, result);
    }

    public async Task<IActionResult> CreateAccount()
    {
        ViewData["Menu"] = (int)MenuDashboard.CreateAccount;
        var users = await _userService.GetAdmin();
        var model = new CreateModelAccount()
        {
            Users = _mapper.Map<IEnumerable<UserChosen>>(users)
        };
        return View(ConstantView.CreateAccount, model);
    }

}