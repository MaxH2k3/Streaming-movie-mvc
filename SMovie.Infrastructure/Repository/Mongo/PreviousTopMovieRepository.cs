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
    public class PreviousTopMovieRepository : MongoRepository<AnalystMovie>, IAnalystMovieRepository
    {
        public PreviousTopMovieRepository(SMovieMongoContext context) : base(context.PreviousTopMovies)
        {

        }
    }
}
