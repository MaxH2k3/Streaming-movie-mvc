using SMovie.Domain.Entity;

namespace SMovie.Application.IService
{
    public interface INationService
    {
        Task<IEnumerable<Nation>> GetNations();
    }
}
