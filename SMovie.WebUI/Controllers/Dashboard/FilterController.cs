using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;
using SMovie.Domain.Constants;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;
using SMovie.WebUI.Constants;

namespace SMovie.WebUI.Controllers.Dashboard
{
    public class FilterController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public FilterController(IUserService userService,
                IMapper mapper,
                IMovieService movieService)
        {
            _userService = userService;
            _mapper = mapper;
            _movieService = movieService;
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

        public async Task<IActionResult> FilterMovie(int page, int eachPage)
        {
            var movies = await _movieService.GetMovies(page, eachPage, MovieSortBy.DateCreated, MovieStatusType.Deleted);
            var result = _mapper.Map<PagedList<InfoMovie>>(movies);
            return View(ConstantComponent.DisplayMovieList, result);
        }

    }
}
