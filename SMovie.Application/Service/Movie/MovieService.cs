using AutoMapper;
using SMovie.Application.IService;
using SMovie.Application.Message;
using SMovie.Application.MessageService;
using SMovie.Domain.Constants;
using SMovie.Domain.Entity;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;
using SMovie.Domain.Repository;
using System.Data;
using System.Net;

namespace SMovie.Application.Service
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedList<Movie>> GetMovieDeleted(int page,int eachPage, string sortBy)
        {
            return await _unitOfWork.MovieRepository.GetAll(m => m.DateDeleted != null, page, eachPage, sortBy);
        }

        public async Task<PagedList<Movie>> GetMoviePending(int page, int eachPage, string sortBy)
        {
            return await _unitOfWork.MovieRepository.GetAll(m => m.Status.Equals(MovieStatus.Pending) && (m.DateDeleted == null), page, eachPage, sortBy);
        }

        public async Task<PagedList<Movie>> GetMovieReleased(int page, int eachPage, string sortBy)
        {
            return await _unitOfWork.MovieRepository.GetAll(m => m.Status.Equals(MovieStatus.Released) && (m.DateDeleted == null), page, eachPage, sortBy);
        }

        public async Task<PagedList<Movie>> GetMovieUpcoming(int page, int eachPage, string sortBy)
        {
            return await _unitOfWork.MovieRepository.GetAll(m => m.Status.Equals(MovieStatus.Upcoming) && (m.DateDeleted == null), page, eachPage, sortBy);
        }

        public async Task<PagedList<Movie>> GetMovies(int page, int eachPage, string sortBy, MovieStatusType exceptBy = MovieStatusType.None)
        {
            if(exceptBy == MovieStatusType.None)
            {
                return await _unitOfWork.MovieRepository.GetAll(page, eachPage, sortBy);
            } else if(exceptBy == MovieStatusType.Deleted)
            {
                return await _unitOfWork!.MovieRepository.GetAll(m => m.DateDeleted == null, page, eachPage, sortBy);
            } else if(exceptBy == MovieStatusType.Pending)
            {
                return await GetMovieReleased(page, eachPage, sortBy);
            } else if(exceptBy == MovieStatusType.Released || exceptBy == MovieStatusType.All)
            {
                return new PagedList<Movie>(new List<Movie>(), 0, page, eachPage);
            } else if(exceptBy == MovieStatusType.Upcoming)
            {
                return await _unitOfWork!.MovieRepository.GetAll(m => (!m.Status.Equals(MovieStatus.Upcoming)) && (m.DateDeleted == null), 
                                                                    page, eachPage, sortBy);
            }
            
            throw new ArgumentException(MessageException.ExceptByNotFound);
        }

        public async Task<Movie?> GetMovieById(int id)
        {
            return await _unitOfWork.MovieRepository.GetById(id);
        }

        public async Task<PagedList<Movie>> GetMovieByPeople(int page, int eachPage, Guid actorId)
        {
            return await _unitOfWork.MovieRepository.GetAll(m => m.Casts!.Any(ma => ma.ActorId.Equals(actorId)), 
                        page, eachPage, MovieSortBy.ProducedDate, false);
        }

        public async Task<PagedList<Movie>> GetMovieByFeature(int page, int eachPage, int featureId)
        {
            return await _unitOfWork.MovieRepository.GetAll(m => m.FeatureId == featureId && m.DateDeleted == null && !m.Status.Equals(MovieStatus.Upcoming), 
                                       page, eachPage, MovieSortBy.ProducedDate, false);
        }

        public async Task<PagedList<Movie>> GetMovieByCategory(int page, int eachPage, int categoryId)
        {
            return await _unitOfWork.MovieRepository.GetAll(m => m.MovieCategories.Any(mc => mc.CategoryId == categoryId) && m.DateDeleted == null && !m.Status.Equals(MovieStatus.Upcoming), 
                                                      page, eachPage, MovieSortBy.ProducedDate, false);
        }

        public async Task<PagedList<Movie>>  GetMovieByNation(int page, int eachPage, string nationId)
        {
            return await _unitOfWork.MovieRepository.GetAll(m => m.NationId.Equals(nationId) && m.DateDeleted == null && !m.Status.Equals(MovieStatus.Upcoming), 
                                                      page, eachPage, MovieSortBy.ProducedDate, false);
        }

        public async Task<PagedList<Movie>> SearchMovie(int page, int eachPage, string key)
        {
            return await _unitOfWork.MovieRepository.GetAll(m => m.EnglishName.ToLower().Contains(key.ToLower()) 
                                   || m.VietnamName.ToLower().Contains(key.ToLower()), page, eachPage, MovieSortBy.ProducedDate);
        }

        public async Task<ResponseDTO> CreateMovie(NewMovie newMovie)
        {
            // Create new movie id
            newMovie.MovieId = Guid.NewGuid();

            // Validate data
            ResponseDTO responseDTO = await ValidateData(newMovie);

            // If validate fail
            if (responseDTO.Status != HttpStatusCode.Continue)
            {
                return responseDTO;
            }

            // Map old movie to new movie
            Movie movie = _mapper.Map<Movie>(newMovie);
            movie.DateCreated = DateTime.Now;

            // Add new movie to database
            await _unitOfWork.MovieRepository.Add(movie);

            // Add movie category
            await _unitOfWork.MovieCategoryRepository.Add(newMovie.MovieId, newMovie.Categories);

            // Save change to database
            if (await _unitOfWork.SaveChangesAsync())
            {
                return new ResponseDTO(HttpStatusCode.Created, MessageCommon.SavingSuccesfully, newMovie.MovieId);
            }

            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, MessageSystem.ServerError);
        }

        public async Task<ResponseDTO> UpdateMovie(NewMovie newMovie)
        {
            // Get movie by id
            Movie? movie = await _unitOfWork.MovieRepository.GetById(newMovie.MovieId);

            // If movie not found
            if (movie == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, MessageMovie.MovieNotFound);
            }

            // Validate data
            ResponseDTO responseDTO = await ValidateData(newMovie);

            // If validate fail
            if (responseDTO.Status != HttpStatusCode.Continue)
            {
                return responseDTO;
            }

            // Map old movie to new movie
            movie = _mapper.Map<Movie>(newMovie);

            // Update movie
            _unitOfWork.MovieRepository.Update(movie);

            // Delete old movie category
            _unitOfWork.MovieCategoryRepository.DeleteMovieCategory(movie.MovieCategories);

            // Add new movie category
            await _unitOfWork.MovieCategoryRepository.Add(newMovie.MovieId, newMovie.Categories);

            // Save change to database
            if (await _unitOfWork.SaveChangesAsync())
            {
                return new ResponseDTO(HttpStatusCode.OK, MessageCommon.UpdateSuccesfully);
            }

            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, MessageSystem.ServerError);
        }

        private async Task<ResponseDTO> ValidateData(NewMovie newMovie)
        {
            // Check nation
            newMovie.NationId = await _unitOfWork.NationRepository.CheckNation(newMovie.NationId);
            if (string.IsNullOrEmpty(newMovie.NationId))
            {
                return new ResponseDTO(HttpStatusCode.NotFound, MessageCommon.NationNotFound);
            }

            // Check feature
            if (await _unitOfWork.FeatureRepository.CheckFeature(newMovie.FeatureId))
            {
                return new ResponseDTO(HttpStatusCode.NotFound, MessageCommon.FeatureFilmNotFound);
            }

            // Check exist movie name
            if (await _unitOfWork.MovieRepository.CheckExistMovieName(newMovie.EnglishName, newMovie.VietnamName, newMovie.MovieId))
            {
                return new ResponseDTO(HttpStatusCode.Conflict, MessageMovie.MovieNotFound);
            }

            return new ResponseDTO(HttpStatusCode.Continue, MessageCommon.ValidateSuccessfully, "");
        }

        public async Task<IEnumerable<Movie>> GetMoveTopViewer()
        {
            return await _unitOfWork.MovieRepository.GetMovieTopViewer(10);
        }

        public async Task<IEnumerable<Movie>> GetMovieTopRating()
        {
            return await _unitOfWork.MovieRepository.GetMovieTopRating(10);
        }

        public async Task<IEnumerable<Movie?>> GetMovieSlide()
        {
            var movies = new LinkedList<Movie?>();
            movies.AddLast(await _unitOfWork.MovieRepository.GetMovieNewest());
            movies.AddLast((await _unitOfWork.MovieRepository.GetMovieTopViewer(1)).FirstOrDefault());
            movies.AddLast((await _unitOfWork.MovieRepository.GetMovieTopRating(1)).FirstOrDefault());

            return movies;
        }

        public async Task<IEnumerable<Movie>> GetTVSeriesDetails()
        {
            return await _unitOfWork.MovieRepository.GetMovieDetails(10, Domain.Enum.FeatureMovie.TVSeries);

        }

        public async Task<IEnumerable<Movie>> GetMovieRecomend()
        {
            var listId = (await _unitOfWork.PreviousTopMovieRepository.GetAll()).Select(p => p.MovieId).ToList();
            return await _unitOfWork.MovieRepository.GetAll(m => listId.Contains(m.MovieId));
        }

        public async Task<Movie?> GetMovieDetail(Guid movieId)
        { 
            return await _unitOfWork.MovieRepository.GetById(movieId);
        }

        public async Task<PagedList<Movie>> GetMovieRelated(Guid movieId, int page, int eachPage)
        {
            return await _unitOfWork.MovieRepository.GetMovieRelated(movieId, page, eachPage);
        }

        public async Task<Dictionary<string, int>> GetStatistic()
        {
            Dictionary<string, int> statistics = new Dictionary<string, int>();
            var movies = await _unitOfWork.MovieRepository.GetAll();
            statistics.Add(MovieStatus.Upcoming.ToString(), movies.Count(m => m.Status.ToLower().Equals(MovieStatus.Upcoming.ToString()) && m.DateDeleted == null));
            statistics.Add(MovieStatus.Pending.ToString(), movies.Count(m => m.Status.ToLower().Equals(MovieStatus.Pending.ToString()) && m.DateDeleted == null));
            statistics.Add(MovieStatus.Released.ToString(), movies.Count(m => m.Status.ToLower().Equals(MovieStatus.Deleted.ToString()) && m.DateDeleted == null));
            statistics.Add(MovieStatus.Deleted.ToString(), movies.Count(m => m.DateDeleted != null));

            return statistics;
        }

    }
}
