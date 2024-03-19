using SMovie.Domain.Entity;

namespace SMovie.Domain.Repository
{
	public interface IPersonRepository : IExtendRepository<Person>
    {
        Task<bool> CheckExistPerson(string namePerson, Guid personId, string roleName);
    }
}
