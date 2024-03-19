using Microsoft.EntityFrameworkCore;
using SMovie.Domain.Entity;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;

namespace SMovie.Infrastructure.Repository
{
    public class FeatureRepository : SQLRepository<FeatureFilm>, IFeatureRepository
    {
        private readonly SMovieSQLContext _context;

        public FeatureRepository(SMovieSQLContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckFeature(int featureId)
        {
            return await _context.FeatureFilms.AnyAsync(x => x.FeatureId == featureId);
        }

    }
}
