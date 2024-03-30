using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;
using SMovie.Domain.Constants;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;
using SMovie.WebUI.Constants;

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
        return View();
    }

    public async Task<IActionResult> MovieList()
    {
        var movies = await _movieService.GetMovies(SystemDefault.Page, SystemDefault.EachPage, MovieSortBy.DateCreated, MovieStatusType.Deleted);
        var result = _mapper.Map<PagedList<MoviePreview>>(movies);
        return View(ConstantView.MovieList, result);
    }

}