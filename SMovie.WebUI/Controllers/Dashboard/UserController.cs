using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;
using SMovie.Domain.Models;
using SMovie.WebUI.Constants;

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
        var users = await _userService.GetUsers();

        if(users == null)
        {
            return View(ConstantView.UserList);
        }

        var result = _mapper.Map<IEnumerable<UserDetail>>(users);

        return View(ConstantView.UserList, result);
    }

    

}