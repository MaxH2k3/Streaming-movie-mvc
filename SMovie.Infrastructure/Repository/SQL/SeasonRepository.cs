using SMovie.Domain.Entity;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;

namespace SMovie.Infrastructure.Repository
{
    public class SeasonRepository : SQLRepository<Season>, ISeasonRepository
    {
        public SeasonRepository(SMovieSQLContext context) : base(context) 
        {
        }
    }
}
