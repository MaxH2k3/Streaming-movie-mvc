using SMovie.Domain.Entity;

namespace SMovie.Domain.Repository
{
    public interface ICastRepository : IRepository<Cast>
    {
        Task AddRange(LinkedList<Cast> casts);
        Task UpdateRange(LinkedList<Cast> casts);
        Task DeleteRange(IEnumerable<Cast>? casts);
    }
}
