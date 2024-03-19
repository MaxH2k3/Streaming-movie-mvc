using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;
using SMovie.Domain.Enum;
using SMovie.Domain.Models.Person;

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

        public IActionResult Index()
        {
            var persons = _mapper.Map<PersonPreview>(_personService.GetPersons(1, 12, PersonSortType.NamePerson));

            return View("../Home/Body/Cast", persons);
        }



    }
}
