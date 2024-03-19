using SMovie.Domain.Models;
using System.Linq.Expressions;

namespace SMovie.Domain.Repository
{
    public interface IExtendRepository<T> : IRepository<T> where T : class
    {

        /// <summary>
        /// Get all entities from database with condition, sort and pagination
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="page"></param>
        /// <param name="eachPage"></param>
        /// <param name="sortBy"></param>
        /// <param name="isAscending"></param>
        /// <returns></returns>
        PagedList<T> GetAll(Expression<Func<T, bool>> predicate, int page, int eachPage, string sortBy, bool isAscending = true);

        /// <summary>
        /// Get all entities from database with sort and pagination
        /// </summary>
        /// <param name="page"></param>
        /// <param name="eachPage"></param>
        /// <param name="sortBy"></param>
        /// <param name="isAscending"></param>
        /// <returns></returns>
        PagedList<T> GetAll(int page, int eachPage, string sortBy, bool isAscending = true);

        /// <summary>
        /// Filter by a property in entity with condition and pagination
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <param name="page"></param>
        /// <param name="eachPage"></param>
        /// <returns></returns>
        Task<PagedList<T>> FilterBy(string propertyName, string propertyValue, int page, int eachPage);

        /// <summary>
        /// Filter by a property in entity with condition, sort and pagination
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <param name="page"></param>
        /// <param name="eachPage"></param>
        /// <param name="sortBy"></param>
        /// <param name="isAscending"></param>
        /// <returns></returns>
        Task<PagedList<T>> FilterBy(string propertyName, string propertyValue, int page, int eachPage, string sortBy = null!, bool isAscending = true);
    }
}
