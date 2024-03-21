using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using SMovie.Application.IService;
using SMovie.Domain.Constants;
using SMovie.Domain.Entity;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;
using SMovie.WebUI.Models;
using System.Reflection;

namespace SMovie.WebUI.Controllers;

public class HomeController : Controller
{
    private readonly IMovieService _movieService;
    private readonly ICategoryService _categoryService;
    private readonly INationService _nationService;
    private readonly IMapper _mapper;

    public HomeController(IMovieService movieService, ICategoryService categoryService,
            IMapper mapper, INationService nationService)
    {
        _movieService = movieService;
        _categoryService = categoryService;
        _mapper = mapper;
        _nationService = nationService;
    }

    public async Task<IActionResult> Index()
    {
        
        var tempResponse = TempData["response"] as string;
        ResponseDTO? responseDTO;
        if(!string.IsNullOrEmpty(tempResponse))
        {
            responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(tempResponse);
            ViewBag.response = responseDTO;
            
        }

        var model = new HomeModel
        {
            // Get movies for slide
            SlideMovies = await _movieService.GetMovieSlide(),

            // Get newest movies
            NewMovies = _mapper.Map<IEnumerable<MoviePreview>>(await _movieService.GetMovies(1, 10, MovieSortType.ProducedDate))
        };

        return View(model);
    }

    public async Task<IActionResult> Genres()
    {
        var categories = await _categoryService.GetCategories();
        var nations = await _nationService.GetNations();

        var model = new { Categories = categories, Nations = nations };

        return View("../Home/Body/Genres", model);
    }

    public IActionResult Movie()
    {
        return View("Body/Movie");
    }

    public IActionResult MovieDetail()
    {
        return View("Body/MovieDetail");
    }

    public IActionResult AccountDetail()
    {
        return View("Body/AccountDetail");
    }

    public IActionResult PricingPlan()
    {
        return View("Body/PricingPlan");
    }

    public IActionResult PersonDetail()
    {
        return View("Body/PersonDetail");
    }

    public IActionResult PlayList()
    {
        return View("Body/PlayList");
    }
}
