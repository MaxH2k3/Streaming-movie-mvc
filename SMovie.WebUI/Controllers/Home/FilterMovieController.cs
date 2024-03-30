using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;
using SMovie.Domain.Models;
using SMovie.WebUI.Constants;

namespace SMovie.WebUI.Controllers.Home
{
    public class FilterMovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public FilterMovieController(IMovieService movieService,
                IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        public async Task<IActionResult> FilterMovieRelated(Guid movieId, int page, int eachPage)
        {
            var movies = await _movieService.GetMovieRelated(movieId, page, eachPage);

            if(movies == null)
            {
                return View(ConstantComponent.DisplaySlide);
            }

            var result = _mapper.Map<PagedList<MoviePreview>>(movies);

            return View(ConstantComponent.DisplayNormal, result);
        }
    }
}
