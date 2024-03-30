using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;
using SMovie.WebUI.Constants;

namespace SMovie.WebUI.Controllers.Dashboard
{
    public class FilterController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public FilterController(IUserService userService,
                IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> FilterUserByStatus(int status, int page, int eachPage)
        {
            var users = await _userService.GetUserByStatus((AccountStatus)status, page, eachPage);

            if (users == null)
            {
                return View(ConstantComponent.DisplayUserList);
            }

            var result = _mapper.Map<PagedList<UserDetail>>(users);

            return View(ConstantComponent.DisplayUserList, result);
        }
    }
}
