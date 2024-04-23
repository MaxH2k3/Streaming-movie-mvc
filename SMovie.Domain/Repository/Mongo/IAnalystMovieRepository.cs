using SMovie.Domain.Models;

namespace SMovie.Domain.Repository
{
    public interface IAnalystMovieRepository : IRepository<AnalystMovie>
    {
        Task UpSert(Guid movieId);
        Task ConvertToPrevious();
    }
}
