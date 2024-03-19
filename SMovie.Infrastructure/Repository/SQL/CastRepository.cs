using SMovie.Domain.Entity;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;

namespace SMovie.Infrastructure.Repository
{
    public class CastRepository : SQLRepository<Cast>, ICastRepository
    {
        public CastRepository(SMovieSQLContext context) : base(context)
        {
        }
    }
}
