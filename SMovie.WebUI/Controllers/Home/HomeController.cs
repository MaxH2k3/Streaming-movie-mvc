﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SMovie.Application.IService;
using SMovie.Domain.Constants;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;
using SMovie.WebUI.Constants;
using SMovie.WebUI.Models;

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
            SlideMovies = _mapper.Map<IEnumerable<MovieSlide>>(await _movieService.GetMovieSlide()),

            // Get newest movies
            NewMovies = _mapper.Map<IEnumerable<MoviePreview>>(await _movieService.GetMovies(SystemDefault.Page, SystemDefault.EachPage, MovieSortBy.ProducedDate, MovieStatusType.Upcoming)),

            // Get movies upcoming
            UpcomingMovies = _mapper.Map<IEnumerable<MoviePreview>>(await _movieService.GetMovieUpcoming(SystemDefault.Page, SystemDefault.EachPage, MovieSortBy.ProducedDate)),

            // Get top 10 movies most viewed
            TopViewedMovies = _mapper.Map<IEnumerable<MoviePreview>>(await _movieService.GetMoveTopViewer()),

            // Get top 10 movies most rating
            TopRatingMovies = _mapper.Map<IEnumerable<MoviePreview>>(await _movieService.GetMovieTopRating()),

            // Get 10 TV Series newest
            NewTVSeries = _mapper.Map<IEnumerable<MovieDetail>>(await _movieService.GetTVSeriesDetails()),

            // Get 10 stand alone movies newest
            NewStandaloneMovies = _mapper.Map<IEnumerable<MovieSlide>>(await _movieService.GetMovieByFeature(SystemDefault.Page, 5, (int)FeatureFilm.Standalone)),

            // Get movie recommend
            RecommendMovies = _mapper.Map<IEnumerable<MoviePreview>>(await _movieService.GetMovieRecomend())

        };

        return View(model);
    }

    public async Task<IActionResult> Genres()
    {
        var categories = await _categoryService.GetCategories();
        var nations = await _nationService.GetNations();

        var model = new { Categories = categories, Nations = nations };

        return View(ConstantView.Genres, model);
    }

    public IActionResult Movie()
    {
        return View(ConstantView.StandaloneMovie);
    }

    public IActionResult AccountDetail()
    {
        return View(ConstantView.AccountDetail);
    }

    public IActionResult PricingPlan()
    {
        return View(ConstantView.PricingPlan);
    }

    public IActionResult PersonDetail()
    {
        return View(ConstantView.PersonDetail);
    }

    public IActionResult PlayList()
    {
        return View(ConstantView.PlayList);
    }

}
