using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;
using SMovie.Domain.Constants;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;
using SMovie.WebUI.Constants;

namespace SMovie.WebUI.Controllers.Home
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public PersonController(IPersonService personService,
                IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var test = await _personService.GetPersons(SystemDefault.Page, 12, PersonSortBy.NamePerson);
            var persons = _mapper.Map<PagedList<PersonPreview>>(test);

            return View(ConstantView.Cast, persons);
        }



    }
}
