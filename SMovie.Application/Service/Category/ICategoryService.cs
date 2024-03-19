
using SMovie.Domain.Entity;

namespace SMovie.Application.IService
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();
    }
}
