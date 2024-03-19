

using SMovie.Domain.Entity;

namespace SMovie.Application.IService
{
    public interface IFeatureService
    {
        Task<IEnumerable<FeatureFilm>> GetFeatures();
    }
}
