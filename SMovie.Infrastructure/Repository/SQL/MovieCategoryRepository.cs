using Microsoft.EntityFrameworkCore;
using SMovie.Domain.Entity;
using SMovie.Domain.Models;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;
using SMovie.Infrastructure.Repository.Common;

namespace SMovie.Infrastructure.Repository
{
    public class MovieCategoryRepository : SQLExtendRepository<MovieCategory>, IMovieCategoryRepository
    {
        private readonly SMovieSQLContext _context;

        public MovieCategoryRepository(SMovieSQLContext context) : base(context)
        {
            _context = context;
        }

        public void DeleteMovieCategory(IEnumerable<MovieCategory> categories)
        {
            _context.MovieCategories.RemoveRange(categories);
        }

        public async Task<IEnumerable<MovieCategory>> GetMovieCategoryByMovieId(Guid movieId)
        {
            return await _context.MovieCategories.Where(x => x.MovieId.Equals(movieId)).ToListAsync();
        }

        public async Task Add(Guid movieId, IEnumerable<int> movieCategories)
        {
            foreach (var categoryId in movieCategories)
            {
                await _context.MovieCategories.AddAsync(new MovieCategory
                {
                    MovieId = movieId,
                    CategoryId = categoryId
                });
            }
        }


    }
}
