using SMovie.Domain.Models;
using SMovie.Domain.Repository.Mongo;
using SMovie.Infrastructure.DBContext;

namespace SMovie.Infrastructure.Repository
{
    public class UserTemporaryRepository : MongoRepository<UserTemporary>, IUserTemporaryRepository
    {
        public UserTemporaryRepository(SMovieMongoContext context) : base(context.Users)
        {
        }
    }
}
