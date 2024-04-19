﻿using SMovie.Domain.Models;
using System.Linq.Expressions;

namespace SMovie.Domain.Repository
{
    public interface IRepository<T> where T : class
    {

        /// <summary>
        /// Get all entities from database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Get all entities from database with pagination
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="eachPage">Number of object want to getting on current page</param>
        /// <returns></returns>
        Task<PagedList<T>> GetAll(int page, int eachPage);

        /// <summary>
        /// Get all entities from database with condition and pagination
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="page"></param>
        /// <param name="eachPage"></param>
        /// <returns></returns>
        Task<PagedList<T>> GetAll(Expression<Func<T, bool>> predicate, int page, int eachPage);

        /// <summary>
        /// Get all entities from database with condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T?> GetById(dynamic id);

        /// <summary>
        /// Add an entity to database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Add(T entity);

        /// <summary>
        /// Update an entity in database
        /// </summary>
        /// <param name="entity"></param>
        Task Update(T entity);

        /// <summary>
        /// Delete an entity from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(dynamic id);

        /// <summary>
        /// Count all entities in database
        /// </summary>
        /// <returns></returns>
        Task<int> Count();

        /// <summary>
        /// Count all entities in database with condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> Count(Expression<Func<T, bool>> predicate);
    }
}
