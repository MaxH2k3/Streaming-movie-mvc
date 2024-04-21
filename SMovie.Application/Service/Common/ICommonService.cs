using SMovie.Domain.Entity;

namespace SMovie.Application.IService
{
    public interface ICommonService
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<FeatureFilm>> GetFeatures();
        Task<IEnumerable<Nation>> GetNations();
        Task<int> TotalCrews();
        Task<int> TotalAccount();
        Task<int> TotalMovie();
        Task<int> TotalCategory();
        Task<bool> TestDelete(string id);
    }
}
