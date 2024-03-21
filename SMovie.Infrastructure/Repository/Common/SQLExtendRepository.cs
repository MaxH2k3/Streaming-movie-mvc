using Microsoft.EntityFrameworkCore;
using SMovie.Domain.Models;
using SMovie.Infrastructure.DBContext;
using SMovie.Infrastructure.Extentions;
using System.Linq.Expressions;

namespace SMovie.Infrastructure.Repository.Common
{
    public class SQLExtendRepository<T> : SQLRepository<T> where T : class
    {
        private readonly SMovieSQLContext _context;

        public SQLExtendRepository(SMovieSQLContext context)
        {
            _context = context;
        }

        public SQLExtendRepository()
        {
            _context = new SMovieSQLContext();
        }

        public async Task<PagedList<T>> GetAll(int page, int eachPage, string sortBy, bool isAscending = false)
        {
            var entities = await _context.Set<T>().PaginateAndSort(page, eachPage, sortBy, isAscending).ToListAsync();

            return new PagedList<T>(entities, entities.Count, page, eachPage);

        }

        public async Task<PagedList<T>> GetAll(Expression<Func<T, bool>> predicate, int page, int eachPage, string sortBy, bool isAscending = true)
        {
            var entities = await _context.Set<T>()
                .Where(predicate)
                .PaginateAndSort(page, eachPage, sortBy, isAscending).ToListAsync();

            return new PagedList<T>(entities, entities.Count, page, eachPage);

        }
    }
}
