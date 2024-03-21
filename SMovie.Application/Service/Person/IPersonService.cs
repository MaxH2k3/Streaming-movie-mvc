

using SMovie.Domain.Entity;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;

namespace SMovie.Application.IService
{
    public interface IPersonService
    {
        Task<PagedList<Person>> GetPersons(int page, int eachPage, PersonSortType sortBy);
        Task<Person?> GetPerson(Guid id);
		Task<PagedList<Person>> GetActors(int page, int eachPage, PersonSortType sortBy);
		Task<PagedList<Person>> GetProducers(int page, int eachPage, PersonSortType sortBy);
		Task<PagedList<Person>> SearchByName(string name, int page, int eachPage, PersonSortType sortBy);
        Task<ResponseDTO> CreatePerson(NewPerson newPerson);
        Task<ResponseDTO> UpdatePerson(NewPerson newPerson);
        Task<ResponseDTO> DeletePerson(Guid id);
    }
}
