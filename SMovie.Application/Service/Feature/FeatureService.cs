using SMovie.Application.IService;
using SMovie.Domain.Entity;
using SMovie.Domain.Repository;

namespace SMovie.Application.Service
{
    public class FeatureService : IFeatureService
	{
		private readonly IUnitOfWork _unitOfWork;

		public FeatureService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<FeatureFilm>> GetFeatures()
		{
			return await _unitOfWork.FeatureRepository.GetAll();
		}
	}
}
