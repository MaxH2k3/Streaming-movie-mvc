using SMovie.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMovie.Domain.Repository
{
    public interface IFeatureRepository : IRepository<FeatureFilm>
    {
        Task<bool> CheckFeature(int featureId);
    }
}
