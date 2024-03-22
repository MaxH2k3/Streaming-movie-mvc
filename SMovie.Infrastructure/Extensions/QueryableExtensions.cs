﻿using SMovie.Infrastructure.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace SMovie.Infrastructure.Extentions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Paginate and sort the queryable
        /// </summary>
        /// <typeparam name="T">The type of the elements in the queryable</typeparam>
        /// <param name="query">The queryable to apply pagination and sorting to</param>
        /// <param name="page">The page number</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <param name="propertyName">The name of the property to sort by</param>
        /// <param name="isAscending">Flag to specify ascending or descending sorting order</param>
        /// <returns>The queryable with pagination and sorting applied</returns>
        /// <exception cref="ArgumentException">Thrown when the property name is null or empty</exception>
        public static IQueryable<T> PaginateAndSort<T>(this IQueryable<T> query, int page, int pageSize, string propertyName, bool isAscending = false)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException("Property name cannot be null or empty.", nameof(propertyName));
            }

            // Create parameter expression for the entity type
            ParameterExpression parameter = Expression.Parameter(typeof(T), string.Empty);

            // Create member expression for accessing the property or field by its name
            MemberExpression property = Expression.PropertyOrField(parameter, propertyName);

            // Create lambda expression to represent the sorting logic
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            // Determine the sorting method based on isAscending flag
            string methodName = isAscending ? "OrderBy" : "OrderByDescending";

            // Create method call expression for the sorting operation
            MethodCallExpression orderByCall = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[2] { typeof(T), property.Type },
                query.Expression,
                Expression.Quote(lambda)
            );

            // Cast the result to IOrderedQueryable<T> to maintain the sorting order
            return (IOrderedQueryable<T>)query.Provider.CreateQuery<T>(orderByCall)
                            .Skip((page - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Apply pagination to the queryable
        /// </summary>
        /// <typeparam name="T">The type of the elements in the queryable</typeparam>
        /// <param name="query">The queryable to apply pagination to</param>
        /// <param name="page">The page number</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <returns>The queryable with pagination applied</returns>
        public static IQueryable<T> Pagination<T>(this IQueryable<T> query, int page, int pageSize)
        {
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }



    }
}
