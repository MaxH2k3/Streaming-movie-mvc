using SMovie.Domain.Models;
using SMovie.Domain.Repository.Mongo;
using SMovie.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMovie.Infrastructure.Repository
{
    public class BlackIPRepository : MongoRepository<BlackIP>, IBlackIPRepository
    {
        public BlackIPRepository(SMovieMongoContext context) : base(context.BlackListIP)
        {
        }
    }
}
