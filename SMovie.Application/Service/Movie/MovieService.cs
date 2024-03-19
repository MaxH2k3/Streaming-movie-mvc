using AutoMapper;
using SMovie.Application.Extension;
using SMovie.Application.IService;
using SMovie.Application.Message;
using SMovie.Application.MessageService;
using SMovie.Domain.Constants;
using SMovie.Domain.Entity;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;
using SMovie.Domain.UnitOfWork;
using SMovie.Infrastructure.UnitOfWork;
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

        public MovieService(IMapper mapper)
        {
            _unitOfWork = new UnitOfWork();
            _mapper = mapper;
        }

        public PagedList<Movie> GetMovieDeleted(int page,int eachPage, MovieSortType sortBy)
        {
            if(sortBy == MovieSortType.EnglishName)
            {
                return _unitOfWork.MovieRepository.GetAll(m => m.DateDeleted != null, page, eachPage, MovieSortBy.EnglishName);
            } else if(sortBy == MovieSortType.DateCreated)
            {
                return _unitOfWork.MovieRepository.GetAll(m => m.DateDeleted != null, page, eachPage, MovieSortBy.DateCreated);
            } else if(sortBy == MovieSortType.DateDeleted)
            {
                return _unitOfWork.MovieRepository.GetAll(m => m.DateDeleted != null, page, eachPage, MovieSortBy.DateDeleted);
            } else if(sortBy == MovieSortType.DateUpdated)
            {
                return _unitOfWork.MovieRepository.GetAll(m => m.DateDeleted != null, page, eachPage, MovieSortBy.DateUpdated);
            } else if(sortBy == MovieSortType.ProducedDate)
            {
                return _unitOfWork.MovieRepository.GetAll(m => m.DateDeleted != null, page, eachPage, MovieSortBy.ProducedDate);
            }

            throw new NotFoundException(MessageMovie.TypeStatusNotFound);
        }

        public PagedList<Movie> GetMoviePending(int page, int eachPage, MovieSortType sortBy)
        {
            if(sortBy == MovieSortType.EnglishName)
            {
                return _unitOfWork.MovieRepository.GetAll(m => m.Status!.Equals(StatusMovie.Pending), page, eachPage, MovieSortBy.EnglishName);
            } else if(sortBy == MovieSortType.DateCreated)
            {
                return _unitOfWork.MovieRepository.GetAll(m => m.Status!.Equals(StatusMovie.Pending), page, eachPage, MovieSortBy.DateCreated);
            } else if(sortBy == MovieSortType.DateDeleted)
            {
                return _unitOfWork.MovieRepository.GetAll(m => m.Status!.Equals(StatusMovie.Pending), page, eachPage, MovieSortBy.DateDeleted);
            } else if(sortBy == MovieSortType.DateUpdated)
            {
                return _unitOfWork.MovieRepository.GetAll(m => m.Status!.Equals(StatusMovie.Pending), page, eachPage, MovieSortBy.DateUpdated);
            } else if(sortBy == MovieSortType.ProducedDate)
            {
                return _unitOfWork.MovieRepository.GetAll(m => m.Status!.Equals(StatusMovie.Pending), page, eachPage, MovieSortBy.ProducedDate);
            }

            throw new NotFoundException(MessageMovie.TypeStatusNotFound);
        }

        public PagedList<Movie> GetMovieReleased(int page, int eachPage, MovieSortType sortBy)
        {
            if(sortBy == MovieSortType.EnglishName)
            {
                return _unitOfWork.MovieRepository.GetAll(m => m.Status!.Equals(StatusMovie.Released), page, eachPage, MovieSortBy.EnglishName);
            } else if(sortBy == MovieSortType.DateCreated)
            {
                return _unitOfWork.MovieRepository.GetAll(m => m.Status!.Equals(StatusMovie.Released), page, eachPage, MovieSortBy.DateCreated);
            } else if(sortBy == MovieSortType.DateDeleted)
            {
                return _unitOfWork.MovieRepository.GetAll(m => m.Status!.Equals(StatusMovie.Released), page, eachPage, MovieSortBy.DateDeleted);
            } else if(sortBy == MovieSortType.DateUpdated)
            {
                return _unitOfWork.MovieRepository.GetAll(m => m.Status!.Equals(StatusMovie.Released), page, eachPage, MovieSortBy.DateUpdated);
            } else if(sortBy == MovieSortType.ProducedDate)
            {
                return _unitOfWork.MovieRepository.GetAll(m => m.Status!.Equals(StatusMovie.Released), page, eachPage, MovieSortBy.ProducedDate);
            }

            throw new NotFoundException(MessageMovie.TypeStatusNotFound);
        }
    
        public PagedList<Movie> GetMovieUpcoming(int page, int eachPage, MovieSortType sortBy)
        {
            if(sortBy == MovieSortType.EnglishName)
            {
                return _unitOfWork.MovieRepository.GetAll(m => m.Status!.Equals(StatusMovie.Upcoming), page, eachPage, MovieSortBy.EnglishName);
            } else if(sortBy == MovieSortType.DateCreated)
            {
                return _unitOfWork.MovieRepository.GetAll(m => m.Status!.Equals(StatusMovie.Upcoming), page, eachPage, MovieSortBy.DateCreated);
            } else if(sortBy == MovieSortType.DateDeleted)
            {
                return _unitOfWork.MovieRepository.GetAll(m => m.Status!.Equals(StatusMovie.Upcoming), page, eachPage, MovieSortBy.DateDeleted);
            } else if(sortBy == MovieSortType.DateUpdated)
            {
                return _unitOfWork.MovieRepository.GetAll(m => m.Status!.Equals(StatusMovie.Upcoming), page, eachPage, MovieSortBy.DateUpdated);
            } else if(sortBy == MovieSortType.ProducedDate)
            {
                return _unitOfWork.MovieRepository.GetAll(m => m.Status!.Equals(StatusMovie.Upcoming), page, eachPage, MovieSortBy.ProducedDate);
            }

            throw new NotFoundException(MessageMovie.TypeStatusNotFound);
        }

        public PagedList<Movie> GetMovies(int page, int eachPage, MovieSortType sortBy)
        {
            if(sortBy == MovieSortType.EnglishName)
            {
                return _unitOfWork.MovieRepository.GetAll(page, eachPage, MovieSortBy.EnglishName);
            } else if(sortBy == MovieSortType.DateCreated)
            {
                return _unitOfWork.MovieRepository.GetAll(page, eachPage, MovieSortBy.DateCreated);
            } else if(sortBy == MovieSortType.DateDeleted)
            {
                return _unitOfWork.MovieRepository.GetAll(page, eachPage, MovieSortBy.DateDeleted);
            } else if(sortBy == MovieSortType.DateUpdated)
            {
                return _unitOfWork.MovieRepository.GetAll(page, eachPage, MovieSortBy.DateUpdated);
            } else if(sortBy == MovieSortType.ProducedDate)
            {
                return _unitOfWork.MovieRepository.GetAll(page, eachPage, MovieSortBy.ProducedDate);
            }   

            throw new NotFoundException(MessageMovie.TypeStatusNotFound);
        }

        public async Task<Movie?> GetMovieById(int id)
        {
            return await _unitOfWork.MovieRepository.GetById(id);
        }

        public PagedList<Movie> GetMovieByPeople(int page, int eachPage, Guid actorId)
        {
            return _unitOfWork.MovieRepository.GetAll(m => m.Casts!.Any(ma => ma.ActorId.Equals(actorId)), 
                        page, eachPage, MovieSortBy.ProducedDate, false);
        }

        public PagedList<Movie> GetMovieByFeature(int page, int eachPage, int featureId)
        {
            return _unitOfWork.MovieRepository.GetAll(m => m.FeatureId == featureId, 
                                       page, eachPage, MovieSortBy.DateCreated, false);
        }

        public PagedList<Movie> GetMovieByCategory(int page, int eachPage, int categoryId)
        {
            return _unitOfWork.MovieRepository.GetAll(m => m.MovieCategories.Any(mc => mc.CategoryId == categoryId), 
                                                      page, eachPage, MovieSortBy.DateCreated, false);
        }

        public PagedList<Movie> GetMovieByNation(int page, int eachPage, string nationId)
        {
            return _unitOfWork.MovieRepository.GetAll(m => m.NationId!.Equals(nationId), 
                                                      page, eachPage, MovieSortBy.DateCreated, false);
        }

        public PagedList<Movie> SearchMovie(int page, int eachPage, string key)
        {
            return _unitOfWork.MovieRepository.GetAll(m => m.EnglishName!.ToLower().Contains(key.ToLower()) 
                                   || m.VietnamName!.ToLower().Contains(key.ToLower()), page, eachPage, MovieSortBy.DateCreated);
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

        public async Task<IEnumerable<MovieSlide>> GetMovieSlide()
        {
            var movies = new LinkedList<MovieSlide>();
            movies.AddLast(_mapper.Map<MovieSlide>(await _unitOfWork.MovieRepository.GetMovieNewest()));
            movies.AddLast(_mapper.Map<MovieSlide>(await _unitOfWork.MovieRepository.GetMovieTopViewer()));
            movies.AddLast(_mapper.Map<MovieSlide>(await _unitOfWork.MovieRepository.GetMovieTopRating()));

            return movies;
        }
    }
}
