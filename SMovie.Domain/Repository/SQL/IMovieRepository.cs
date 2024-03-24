using SMovie.Domain.Entity;

namespace SMovie.Domain.Repository
{
    public interface IMovieRepository : IExtendRepository<Movie>
    {
        Task<bool> CheckExistMovieName(string englishName, string vietnamName, Guid id);
        Task<IEnumerable<Movie>> GetMovieTopViewer(int amount);
        Task<IEnumerable<Movie>> GetMovieTopRating(int amount);
        Task<Movie?> GetMovieNewest();
        Task<IEnumerable<Movie>> GetMovieDetails(int amount, Domain.Enum.FeatureFilm feature);
    }
}
