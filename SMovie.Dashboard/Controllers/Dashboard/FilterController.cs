using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;
using SMovie.Dashboard.Constants;
using SMovie.Domain.Constants;
using SMovie.Domain.Entity;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;

namespace SMovie.Dashboard.Controllers
{
    public class FilterController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;
        private readonly IPersonService _personService;
        
        public FilterController(IUserService userService,
                IMapper mapper, IPersonService personService,
                IMovieService movieService)
        {
            _userService = userService;
            _mapper = mapper;
            _movieService = movieService;
            _personService = personService;
        }

        public async Task<IActionResult> FilterUserByStatus(int status, int page, int eachPage)
        {
            var users = await _userService.GetUserByStatus((AccountStatus)status, page, eachPage);

            var result = _mapper.Map<PagedList<UserDetail>>(users);

            return View(ConstantComponent.DisplayUserList, result);
        }

        public async Task<IActionResult> FilterMovie(int page, int eachPage)
        {
            var movies = await _movieService.GetMovies(page, eachPage, MovieSortBy.DateCreated, MovieStatusType.Deleted);
            var result = _mapper.Map<PagedList<InfoMovie>>(movies);
            return View(ConstantComponent.DisplayMovieList, result);
        }

        public async Task<PagedList<Person>> SearchPersonName(string name, int page, int eachPage)
        {
            PagedList<Person> persons;
            if (string.IsNullOrEmpty(name))
            {
                persons = await _personService.GetPersons(page, eachPage, PersonSortBy.NamePerson);
            } else
            {
                persons = await _personService.SearchByName(name, page, eachPage, PersonSortBy.NamePerson);
            }
            
            return persons;
        }
        
    }
}
