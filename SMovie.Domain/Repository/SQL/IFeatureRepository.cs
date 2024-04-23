using SMovie.Domain.Entity;

namespace SMovie.Domain.Repository
{
    public interface IFeatureRepository : IRepository<FeatureFilm>
    {
        Task<bool> CheckFeature(int featureId);
    }
}
