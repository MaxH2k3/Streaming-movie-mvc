using Microsoft.EntityFrameworkCore;
using SMovie.Domain.Constants;
using SMovie.Domain.Entity;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;
using SMovie.Infrastructure.Extentions;
using SMovie.Infrastructure.Repository.Common;
using System.Data;
using System.Linq.Expressions;

namespace SMovie.Infrastructure.Repository
{
    public class MovieRepository : SQLExtendRepository<Movie>, IMovieRepository
    {
        private readonly SMovieSQLContext _context;

        public MovieRepository(SMovieSQLContext context) : base(context)
        {
            _context = context;
        }

        public void GetTest()
        {
            var movies = _context.Movies.PaginateAndSort(1, 10, MovieSortBy.ProducedDate);
            foreach (var item in movies)
            {
                Console.WriteLine(item.EnglishName);
            }
        }

        public new PagedList<Movie> GetAll(Expression<Func<Movie, bool>> predicate, int page, int eachPage, string sortBy, bool isAscending = false)
        {
            var parameter = Expression.Parameter(typeof(Movie), "x");
            var property = Expression.Property(parameter, sortBy);
            var lambda = Expression.Lambda<Func<Movie, object>>(property, parameter);
            var sortExpression = lambda.Compile();

            if (isAscending)
            {
                var list = _context.Movies
                    .Include(m => m.MovieCategories).ThenInclude(mc => mc.Category)
                    .Include(m => m.Nation)
                    .Include(m => m.Feature)
                    .Where(predicate).OrderBy(sortExpression).ToList();
                var totalItems = list.Count;
                var items = list.Skip((page - 1) * eachPage).Take(eachPage);

                return new PagedList<Movie>(items, totalItems, page, eachPage);
            }

            var listDesc = _context.Movies.Where(predicate).OrderByDescending(sortExpression).ToList();
            var totalItemsDesc = listDesc.Count;
            var itemsDesc = listDesc.Skip((page - 1) * eachPage).Take(eachPage);

            return new PagedList<Movie>(itemsDesc, totalItemsDesc, page, eachPage);
        }

        public async Task<bool> CheckExistMovieName(string englishName, string vietnamName, Guid id)
        {
                return await _context.Movies.AnyAsync(x => x.EnglishName!.ToLower().Equals(englishName.ToLower()) 
                                   && x.VietnamName!.ToLower().Equals(vietnamName.ToLower()) && !x.MovieId.Equals(id));
        }

        public async Task<Movie?> GetMovieNewest()
        {
            GetTest();
            return await _context.Movies
                .Include(m => m.Feature)
                .Include(m => m.MovieCategories).ThenInclude(c => c.Category)
                .Where(m => m.DateDeleted == null && !m.Status!.Equals(StatusMovie.Upcoming))
                .OrderByDescending(m => m.ProducedDate)
                .FirstOrDefaultAsync();
        }

        public async Task<Movie?> GetMovieTopRating()
        {
            return await _context.Movies
                .Include(m => m.Feature)
                .Include(m => m.MovieCategories).ThenInclude(c => c.Category)
                .Where(m => m.DateDeleted == null && !m.Status!.Equals(StatusMovie.Upcoming))
                .OrderByDescending(m => m.Mark)
                .FirstOrDefaultAsync();
        }

        public async Task<Movie?> GetMovieTopViewer()
        {
            return await _context.Movies
                .Include(m => m.Feature)
                .Include(m => m.MovieCategories).ThenInclude(c => c.Category)
                .Where(m => m.DateDeleted == null && !m.Status!.Equals(StatusMovie.Upcoming))
                .OrderByDescending(m => m.Viewer)
                .FirstOrDefaultAsync();
        }

    }
}
