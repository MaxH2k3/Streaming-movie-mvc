using SMovie.Domain.Entity;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMovie.Infrastructure.Repository
{
    public class EpisodeRepository : SQLRepository<Episode>, IEpisodeRepository
    {
        public EpisodeRepository(SMovieSQLContext context) : base(context)
        {
        }
    }
}
