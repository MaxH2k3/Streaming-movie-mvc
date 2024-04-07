using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;
using SMovie.Dashboard.Constants;
using SMovie.Dashboard.Enums;
using SMovie.Dashboard.Models;
using SMovie.Domain.Constants;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;

namespace SMovie.Dashboard.Controllers;

public class DashboardController : Controller
{
    private readonly IMovieService _movieService;
    private readonly IIPService _ipService;
    private readonly INationService _nationService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public DashboardController(IMovieService movieService,
                IMapper mapper, INationService nationService,
                IIPService ipService, ICategoryService categoryService)
    {
        _movieService = movieService;
        _mapper = mapper;
        _ipService = ipService;
        _nationService = nationService;
        _categoryService = categoryService;
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

    public async Task<IActionResult> CreateMovie()
    {
        ViewData["Menu"] = (int)MenuDashboard.CreateMovie;

        var model = new CreateModelMovie
        {
            Categories = await _categoryService.GetCategories(),
            Nations = await _nationService.GetNations()
        };

        return View(ConstantView.CreateMovie, model);
    }

    public async Task<IActionResult> IpAddress()
    {
        ViewData["Menu"] = (int)MenuDashboard.IpAddress;
        var ips = await _ipService.GetIPAddress();
        return View(ConstantView.IPAdress, ips);
    }

}