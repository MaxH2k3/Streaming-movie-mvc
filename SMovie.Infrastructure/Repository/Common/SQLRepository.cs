using Microsoft.EntityFrameworkCore;
using SMovie.Domain.Models;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;
using System.Linq.Expressions;

namespace SMovie.Infrastructure.Repository
{
    public class SQLRepository<T> : IRepository<T> where T : class
    {
        private readonly SMovieSQLContext _context;

        public SQLRepository(SMovieSQLContext context)
        {
            _context = context;
        }

        public SQLRepository()
        {
            _context = new SMovieSQLContext();
        }

        ///Base Repository

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task Delete(dynamic id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return;
            }
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _context.Set<T>().Attach(entity);
                entry.State = EntityState.Modified;
            }
        }

        public async Task<T?> GetById(dynamic id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<PagedList<T>> GetAll(int page, int eachPage)
        {
            var list = await _context.Set<T>().ToListAsync();
            var totalItems = list.Count;
            var items = list.Skip((page - 1) * eachPage).Take(eachPage);

            return new PagedList<T>(items, totalItems, page, eachPage);
        }

        public async Task<PagedList<T>> GetAll(Expression<Func<T, bool>> predicate, int page, int eachPage)
        {
            var list = await _context.Set<T>().Where(predicate).ToListAsync();
            var totalItems = list.Count;
            var items = list.Skip((page - 1) * eachPage).Take(eachPage);

            return new PagedList<T>(items, totalItems, page, eachPage);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }
	}
}
