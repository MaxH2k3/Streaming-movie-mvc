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

        public new async Task<PagedList<Movie>> GetAll(Expression<Func<Movie, bool>> predicate, int page, int eachPage, string sortBy, bool isAscending = false)
        {
            var movies = await _context.Movies
                .Include(m => m.Feature)
                .Include(m => m.MovieCategories).ThenInclude(c => c.Category)
                .Where(predicate)
                .PaginateAndSort(page, eachPage, sortBy, isAscending)
                .ToListAsync();

            return new PagedList<Movie>(movies, movies.Count, page, eachPage);
        }

        public async Task<bool> CheckExistMovieName(string englishName, string vietnamName, Guid id)
        {
                return await _context.Movies.AnyAsync(x => x.EnglishName!.ToLower().Equals(englishName.ToLower()) 
                                   && x.VietnamName!.ToLower().Equals(vietnamName.ToLower()) && !x.MovieId.Equals(id));
        }

        public async Task<Movie?> GetMovieNewest()
        {
            return await _context.Movies
                .Include(m => m.Feature)
                .Include(m => m.MovieCategories).ThenInclude(c => c.Category)
                .Where(m => m.DateDeleted == null && !m.Status!.Equals(MovieStatus.Upcoming))
                .OrderByDescending(m => m.ProducedDate)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Movie>> GetMovieTopRating(int amount = 10)
        {
            return await _context.Movies
                .Include(m => m.Feature)
                .Include(m => m.MovieCategories).ThenInclude(c => c.Category)
                .Where(m => m.DateDeleted == null && !m.Status.Equals(MovieStatus.Upcoming))
                .OrderByDescending(m => m.Mark)
                .Take(amount)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetMovieTopViewer(int amount)
        {
            return await _context.Movies
                .Include(m => m.Feature)
                .Include(m => m.MovieCategories).ThenInclude(c => c.Category)
                .Where(m => m.DateDeleted == null && !m.Status.Equals(MovieStatus.Upcoming))
                .OrderByDescending(m => m.Viewer)
                .Take(amount)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetMovieDetails(int amount, Domain.Enum.FeatureFilm feature)
        {
            return await _context.Movies
                .Include(m => m.Nation)
                .Include(m => m.Feature)
                .Include(m => m.MovieCategories).ThenInclude(c => c.Category)
                .Include(m => m.Casts).ThenInclude(ma => ma.Actor)
                .Include(m => m.Seasons).ThenInclude(s => s.Episodes)
                .Where(m => m.DateDeleted == null && !m.Status!.Equals(MovieStatus.Upcoming) && m.FeatureId == (int)feature)
                .OrderByDescending(m => m.ProducedDate)
                .Take(amount)
                .ToListAsync();
        }

    }
}
