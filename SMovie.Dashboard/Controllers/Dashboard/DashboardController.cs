using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;
using SMovie.Dashboard.Constants;
using SMovie.Dashboard.Enums;
using SMovie.Dashboard.Models;
using SMovie.Domain.Constants;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;

namespace SMovie.Dashboard.Controllers;

[Authorize(Policy = "Admin")]

public class DashboardController : Controller
{
    private readonly IMovieService _movieService;
    private readonly IIPService _ipService;
    private readonly ICommonService _commonService;
    private readonly IPersonService _personService;
    private readonly IMapper _mapper;

    public DashboardController(IMovieService movieService,
                IMapper mapper, ICommonService commonService,
                IIPService ipService, IPersonService personService)
    {
        _movieService = movieService;
        _mapper = mapper;
        _ipService = ipService;
        _commonService = commonService;
        _personService = personService;
    }

    public async Task<IActionResult> IndexAsync()
    {
        ViewData["Menu"] = 0;

        DashboardData model = new()
        {
            ListMovieCategory = await _movieService.GetNumOfMovieByCategory(),
            ListMovieTop = await _movieService.GetCurrentTopMovie(),
            ListMovieOnPage = await _movieService.GetStatistic()
        };

        return View(model);
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

    public async Task<IActionResult> CreateMoviePage()
    {
        ViewData["Menu"] = (int)MenuDashboard.CreateMovie;

        var model = new CreateModelMovie
        {
            Categories = await _commonService.GetCategories(),
            Nations = await _commonService.GetNations(),
            Persons = _mapper.Map<PagedList<PersonPreview>>(await _personService.GetPersons(SystemDefault.Page, SystemDefault.EachPage, PersonSortBy.NamePerson))
        };

        return View(ConstantView.CreateMovie, model);
    }

    public async Task<IActionResult> IpAddress()
    {
        ViewData["Menu"] = (int)MenuDashboard.IpAddress;
        var ips = await _ipService.GetIPAddress();
        return View(ConstantView.IPAdress, ips);
    }

    public async Task<IActionResult> DeleteMovie(Guid movieId, bool isPermanently)
    {
        if(isPermanently)
        {
            await _movieService.DeleteMovie(movieId);
        }
        else
        {
            await _movieService.UpdateStatusMovie(movieId, MovieStatus.Deleted);
        }

        ViewData["Menu"] = (int)MenuDashboard.MovieList;
        var movies = await _movieService.GetMovies(SystemDefault.Page, 999999, MovieSortBy.DateCreated, MovieStatusType.Deleted);
        var result = _mapper.Map<PagedList<InfoMovie>>(movies);

        return View(ConstantView.MovieList, result);
    }

    public async Task<IActionResult> RestoreMovie(Guid movieId)
    {
        await _movieService.UpdateStatusMovie(movieId, MovieStatus.Reverted);

        ViewData["Menu"] = (int)MenuDashboard.Trash;
        var movies = await _movieService.GetMovieDeleted(SystemDefault.Page, 999999, MovieSortBy.DateCreated);
        var result = _mapper.Map<PagedList<InfoMovie>>(movies);
        return View(ConstantView.MovieList, result);
    }

}