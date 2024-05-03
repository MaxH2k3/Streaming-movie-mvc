using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;

namespace SMovie.Infrastructure.Repository
{
    public class GeminiRepository : MongoRepository<string>, IGeminiRepository
    {
        public GeminiRepository(SMovieMongoContext context) : base(context.Geminis)
        {
        }
    }
}
