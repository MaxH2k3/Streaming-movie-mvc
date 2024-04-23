using SMovie.Domain.Models;
using SMovie.Infrastructure.DBContext;
using SMovie.Domain.Repository;

namespace SMovie.Infrastructure.Repository
{
    public class VerifyTokenRepository : MongoRepository<VerifyToken>, IVerifyTokenRepository
    {
        public VerifyTokenRepository(SMovieMongoContext context) : base(context.Tokens)
        {
        }
    }
}
