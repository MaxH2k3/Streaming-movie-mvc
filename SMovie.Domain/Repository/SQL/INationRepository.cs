using SMovie.Domain.Entity;

namespace SMovie.Domain.Repository
{
    public interface INationRepository : IRepository<Nation>
    {
        Task<string> CheckNation(string nationId);
    }
}
