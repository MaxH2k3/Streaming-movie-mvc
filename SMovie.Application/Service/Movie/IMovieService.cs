

using SMovie.Domain.Entity;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;

namespace SMovie.Application.IService
{
    public interface IMovieService
    {
        Task<PagedList<Movie>> GetMovieDeleted(int page, int eachPage, MovieSortType sortBy);
        Task<PagedList<Movie>> GetMoviePending(int page, int eachPage, MovieSortType sortBy);
        Task<PagedList<Movie>> GetMovieReleased(int page, int eachPage, MovieSortType sortBy);
        Task<PagedList<Movie>> GetMovieUpcoming(int page, int eachPage, MovieSortType sortBy);
        Task<PagedList<Movie>> GetMovies(int page, int eachPage, MovieSortType sortBy);
        Task<Movie?> GetMovieById(int id);
        Task<PagedList<Movie>> GetMovieByPeople(int page, int eachPage, Guid actorId);
        Task<PagedList<Movie>> GetMovieByFeature(int page, int eachPage, int featureId);
        Task<PagedList<Movie>> GetMovieByCategory(int page, int eachPage, int categoryId);
        Task<PagedList<Movie>> GetMovieByNation(int page, int eachPage, string nationId);
        Task<PagedList<Movie>> SearchMovie(int page, int eachPage, string key);
        Task<ResponseDTO> CreateMovie(NewMovie newMovie);
        Task<ResponseDTO> UpdateMovie(NewMovie newMovie);
        Task<IEnumerable<MovieSlide>> GetMovieSlide();
    }
}
