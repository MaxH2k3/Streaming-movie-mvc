using SMovie.Domain.Models;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;

namespace SMovie.Infrastructure.Repository
{
    public class BlackIPRepository : MongoRepository<BlackIP>, IBlackIPRepository
    {
        public BlackIPRepository(SMovieMongoContext context) : base(context.BlackListIP)
        {
        }
    }
}
