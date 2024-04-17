using SMovie.Domain.Entity;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;

namespace SMovie.Infrastructure.Repository
{
    public class CastRepository : SQLRepository<Cast>, ICastRepository
    {
        private readonly SMovieSQLContext _context;

        public CastRepository(SMovieSQLContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddRange(LinkedList<Cast> casts)
        {
            await _context.Casts.AddRangeAsync(casts);
        }

        public Task UpdateRange(LinkedList<Cast> casts)
        {
            _context.Casts.UpdateRange(casts);

            return Task.CompletedTask;
        }

        public Task DeleteRange(IEnumerable<Cast>? casts)
        {
            if(casts == null)
                return Task.CompletedTask;

            _context.Casts.RemoveRange(casts);

            return Task.CompletedTask;
        }
    }
}
