using SMovie.Domain.Entity;

namespace SMovie.Domain.Repository
{
    public interface IMovieRepository : IExtendRepository<Movie>
    {
        Task<bool> CheckExistMovieName(string englishName, string vietnamName, Guid id);
        Task<Movie?> GetMovieTopViewer();
        Task<Movie?> GetMovieTopRating();
        Task<Movie?> GetMovieNewest();
    }
}
