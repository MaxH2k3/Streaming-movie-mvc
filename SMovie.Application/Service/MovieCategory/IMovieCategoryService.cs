
using SMovie.Domain.Entity;
using SMovie.Domain.Models;

namespace SMovie.Application.IService
{
    public interface IMovieCategoryService
    {
        Task<ResponseDTO> CreateMovieCategory(Guid movieId, IEnumerable<int> MovieCategories);
        Task<IEnumerable<MovieCategory>> GetMovieCategories(Guid movieId);
        Task<ResponseDTO> UpdateMovieCategory(Guid movieId, IEnumerable<int> MovieCategories);
        Task<ResponseDTO> DeleteCategoryByMovie(Guid movieId);
    }
}
