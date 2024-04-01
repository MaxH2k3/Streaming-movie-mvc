using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;
using SMovie.Domain.Constants;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;
using SMovie.WebUI.Constants;
using SMovie.WebUI.Enums;

namespace SMovie.WebUI.Controllers.Dashboard;

public class DashboardController : Controller
{
    private readonly IMovieService _movieService;
    private readonly IMapper _mapper;

    public DashboardController(IMovieService movieService,
                IMapper mapper)
    {
        _movieService = movieService;
        _mapper = mapper;
    }
    
    public IActionResult Index()
    {
        ViewData["Menu"] = 0;
        return View();
    }

    public async Task<IActionResult> MovieList()
    {
        ViewData["Menu"] = (int)MenuDashboard.MovieList;
        var movies = await _movieService.GetMovies(SystemDefault.Page, 999999, MovieSortBy.DateCreated, MovieStatusType.Deleted);
        var result = _mapper.Map<PagedList<InfoMovie>>(movies);
        return View(ConstantView.MovieList, result);
    }

    public async Task<IActionResult> ListMovieDeleted()
    {
        ViewData["Menu"] = (int)MenuDashboard.Trash;
        var movies = await _movieService.GetMovieDeleted(SystemDefault.Page, 999999, MovieSortBy.DateCreated);
        var result = _mapper.Map<PagedList<InfoMovie>>(movies);
        return View(ConstantView.MovieList, result);
    }


}