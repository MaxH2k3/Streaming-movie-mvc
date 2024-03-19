

using SMovie.Domain.Entity;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;

namespace SMovie.Application.IService
{
    public interface IMovieService
    {
        PagedList<Movie> GetMovieDeleted(int page, int eachPage, MovieSortType sortBy);
        PagedList<Movie> GetMoviePending(int page, int eachPage, MovieSortType sortBy);
        PagedList<Movie> GetMovieReleased(int page, int eachPage, MovieSortType sortBy);
        PagedList<Movie> GetMovieUpcoming(int page, int eachPage, MovieSortType sortBy);
        PagedList<Movie> GetMovies(int page, int eachPage, MovieSortType sortBy);
        Task<Movie?> GetMovieById(int id);
        PagedList<Movie> GetMovieByPeople(int page, int eachPage, Guid actorId);
        PagedList<Movie> GetMovieByFeature(int page, int eachPage, int featureId);
        PagedList<Movie> GetMovieByCategory(int page, int eachPage, int categoryId);
        PagedList<Movie> GetMovieByNation(int page, int eachPage, string nationId);
        PagedList<Movie> SearchMovie(int page, int eachPage, string key);
        Task<ResponseDTO> CreateMovie(NewMovie newMovie);
        Task<ResponseDTO> UpdateMovie(NewMovie newMovie);
        Task<IEnumerable<MovieSlide>> GetMovieSlide();
    }
}
