using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;
using SMovie.Dashboard.Constants;
using SMovie.Dashboard.Enums;
using SMovie.Domain.Models;
using SMovie.Dashboard.Models;

namespace SMovie.Dashboard.Controllers;

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

    public async Task<IActionResult> CreateAccountPage()
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