using Microsoft.EntityFrameworkCore;
using SMovie.Domain.Constants;
using SMovie.Domain.Entity;
using SMovie.Domain.Models;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;
using SMovie.Infrastructure.Extentions;
using SMovie.Infrastructure.Repository.Common;
using System.Linq.Expressions;

namespace SMovie.Infrastructure.Repository
{
	public class PersonRepository : SQLExtendRepository<Person>, IPersonRepository
    {
		private readonly SMovieSQLContext _context;

        public PersonRepository(SMovieSQLContext context) : base(context)
        {
			_context = context;
        }

		public async Task<bool> CheckExistPerson(string namePerson, Guid personId, string roleName)
		{
			return await _context.Persons.AnyAsync(x => x.NamePerson!.ToLower().Equals(namePerson.ToLower()) 
					&& x.Role!.Equals(roleName) && !x.PersonId.Equals(personId));
		}

        public new async Task<PagedList<Person>> GetAll(int page, int eachPage, string sortBy, bool isAscending = true)
        {
            var entities = await _context.Persons
                .Include(p => p.Nation)
                .PaginateAndSort(page, eachPage, sortBy, isAscending).ToListAsync();

            return new PagedList<Person>(entities, entities.Count, page, eachPage);

        }

    }
}
