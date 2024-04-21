using SMovie.Application.IService;
using SMovie.Domain.Entity;
using SMovie.Domain.Repository;

namespace SMovie.Application.Service
{
    public class CommonService : ICommonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _unitOfWork.CategoryRepository.GetAll();
        }

        public async Task<IEnumerable<FeatureFilm>> GetFeatures()
        {
            return await _unitOfWork.FeatureRepository.GetAll();
        }

        public async Task<IEnumerable<Nation>> GetNations()
        {
            return await _unitOfWork.NationRepository.GetAll();
        }

        /*public async Task<Dictionary<string, int>> GetAnalystUserMovie()
        {
            // Total cast/crews
            // Total account
            // Total movie
            // Total category
        }*/

        public async Task<int> TotalCrews()
        {
            return await _unitOfWork.PersonRepository.Count();
        }

        public async Task<int> TotalAccount()
        {
            return await _unitOfWork.UserRepository.Count();
        }

        public async Task<int> TotalMovie()
        {
            return await _unitOfWork.MovieRepository.Count();
        }

        public async Task<int> TotalCategory()
        {
            return await _unitOfWork.CategoryRepository.Count();
        }

    }
}
