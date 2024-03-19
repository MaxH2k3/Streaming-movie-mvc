

using Microsoft.EntityFrameworkCore;
using SMovie.Application.IService;
using SMovie.Domain.Models;
using SMovie.Domain.Entity;
using SMovie.Domain.UnitOfWork;
using System.Net;
using SMovie.Application.MessageService;
using SMovie.Infrastructure.UnitOfWork;

namespace SMovie.Application.Service
{
    public class MovieCategoryService : IMovieCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MovieCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public MovieCategoryService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<ResponseDTO> CreateMovieCategory(Guid movieId, IEnumerable<int> movieCategories)
        {
            foreach (var categoryId in movieCategories)
            {
                MovieCategory movieCategory = new()
                {
                    MovieId = movieId,
                    CategoryId = categoryId
                };

                await _unitOfWork.MovieCategoryRepository.Add(movieCategory);
            }
            if(await _unitOfWork.SaveChangesAsync())
            {
                return new ResponseDTO(HttpStatusCode.OK, MessageCommon.SavingSuccesfully);
            }

            return new ResponseDTO(HttpStatusCode.NotModified, MessageCommon.SavingFailed);
        }

        public async Task<ResponseDTO> DeleteCategoryByMovie(Guid movieId)
        {
            IEnumerable<MovieCategory> movieCategories = await GetMovieCategories(movieId);
            if (!movieCategories.Any())
            {
                return new ResponseDTO(HttpStatusCode.NotFound, MessageCategory.CategoryNotFound);
            }

            _unitOfWork.MovieCategoryRepository.DeleteMovieCategory(movieCategories);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return new ResponseDTO(HttpStatusCode.OK, MessageCommon.SavingSuccesfully);
            }

            return new ResponseDTO(HttpStatusCode.NotModified, MessageCommon.DeleteFailed);
        }

        public async Task<IEnumerable<MovieCategory>> GetMovieCategories(Guid movieId)
        {
            return await _unitOfWork.MovieCategoryRepository.GetAll(mc => mc.MovieId.Equals(movieId));
        }

        public async Task<ResponseDTO> UpdateMovieCategory(Guid movieId, IEnumerable<int> MovieCategories)
        {
            //get categories of movie
            IEnumerable<MovieCategory> movieCategories = await GetMovieCategories(movieId);

            foreach (var categoryId in MovieCategories)
            {
                MovieCategory movieCategory = new() 
                { 
                    MovieId = movieId,
                    CategoryId = categoryId
                };

                if (!movieCategories.Contains(movieCategory))
                {
                    await _unitOfWork.MovieCategoryRepository.Add(movieCategory);
                }
            }

            if (await _unitOfWork.SaveChangesAsync())
            {
                return new ResponseDTO(HttpStatusCode.OK, MessageCommon.UpdateSuccesfully);
            }

            return new ResponseDTO(HttpStatusCode.NotModified, MessageCommon.UpdateFailed);
        }

    }
}
