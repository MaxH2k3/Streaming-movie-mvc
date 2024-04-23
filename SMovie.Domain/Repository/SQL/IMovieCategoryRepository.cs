using SMovie.Domain.Entity;

namespace SMovie.Domain.Repository
{
    public interface IMovieCategoryRepository : IExtendRepository<MovieCategory>
    {
        void DeleteMovieCategory(IEnumerable<MovieCategory> categories);
        Task<IEnumerable<MovieCategory>> GetMovieCategoryByMovieId(Guid movieId);
        Task Add(Guid movieId, IEnumerable<int> movieCategories);
    }
}
