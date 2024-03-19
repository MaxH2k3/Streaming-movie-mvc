using Microsoft.EntityFrameworkCore;
using SMovie.Domain.Models;
using SMovie.Infrastructure.DBContext;
using System.Linq.Expressions;
using System.Reflection;

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

        public PagedList<T> GetAll(int page, int eachPage, string sortBy, bool isAscending = true)
        {

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, sortBy);
            var lambda = Expression.Lambda<Func<T, object>>(property, parameter);
            var sortExpression = lambda.Compile();

            if (isAscending)
            {
                var list = _context.Set<T>().OrderBy(sortExpression).ToList();
                var totalItems = list.Count;
                var items = list.Skip((page - 1) * eachPage).Take(eachPage);

                return new PagedList<T>(items, totalItems, page, eachPage);
            }

            var listDesc = _context.Set<T>().OrderByDescending(sortExpression).ToList();
            var totalItemsDesc = listDesc.Count;
            var itemsDesc = listDesc.Skip((page - 1) * eachPage).Take(eachPage);

            return new PagedList<T>(itemsDesc, totalItemsDesc, page, eachPage);

        }

        public PagedList<T> GetAll(Expression<Func<T, bool>> predicate, int page, int eachPage, string sortBy, bool isAscending = true)
        {

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, sortBy);
            var lambda = Expression.Lambda<Func<T, object>>(property, parameter);
            var sortExpression = lambda.Compile();

            if (isAscending)
            {
                var list = _context.Set<T>().Where(predicate).OrderBy(sortExpression).ToList();
                var totalItems = list.Count;
                var items = list.Skip((page - 1) * eachPage).Take(eachPage);

                return new PagedList<T>(items, totalItems, page, eachPage);
            }

            var listDesc = _context.Set<T>().Where(predicate).OrderByDescending(sortExpression).ToList();
            var totalItemsDesc = listDesc.Count;
            var itemsDesc = listDesc.Skip((page - 1) * eachPage).Take(eachPage);

            return new PagedList<T>(itemsDesc, totalItemsDesc, page, eachPage);

        }

        public async Task<PagedList<T>> FilterBy(string propertyName, string propertyValue, int page, int eachPage)
        {   
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var constant = Expression.Constant(propertyValue);
            var equal = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<T, bool>>(equal, parameter);

            var list = await _context.Set<T>().Where(lambda).ToListAsync();
            var totalItems = list.Count;
            var items = list.Skip((page - 1) * eachPage).Take(eachPage);

            return new PagedList<T>(items, totalItems, page, eachPage);
        }

        public async Task<PagedList<T>> FilterBy(string propertyName, string propertyValue, int page, int eachPage, string sortBy = null!, bool isAscending = true)
        {

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var constant = Expression.Constant(propertyValue);
            var equal = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<T, bool>>(equal, parameter);

            if(sortBy != null)
            {
                var sortProperty = Expression.Property(parameter, sortBy);
                var sortLambda = Expression.Lambda<Func<T, object>>(sortProperty, parameter);
                var sortExpression = sortLambda.Compile();

                if (isAscending)
                {
                    var list = _context.Set<T>().Where(lambda).OrderBy(sortExpression).ToList();
                    var totalItems = list.Count;
                    var items = list.Skip((page - 1) * eachPage).Take(eachPage);

                    return new PagedList<T>(items, totalItems, page, eachPage);
                }

                var listDesc = _context.Set<T>().Where(lambda).OrderByDescending(sortExpression).ToList();
                var totalItemsDesc = listDesc.Count;
                var itemsDesc = listDesc.Skip((page - 1) * eachPage).Take(eachPage);

                return new PagedList<T>(itemsDesc, totalItemsDesc, page, eachPage);
            }

            var listNoSort = await _context.Set<T>().Where(lambda).ToListAsync();
            var totalItemsNoSort = listNoSort.Count;
            var itemsNoSort = listNoSort.Skip((page - 1) * eachPage).Take(eachPage);

            return new PagedList<T>(itemsNoSort, totalItemsNoSort, page, eachPage);
        }
    }
}
