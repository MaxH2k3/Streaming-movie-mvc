using SMovie.Application.IService;
using SMovie.Domain.Entity;
using SMovie.Domain.UnitOfWork;
using SMovie.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMovie.Application.Service
{
	public class FeatureService : IFeatureService
	{
		private readonly IUnitOfWork _unitOfWork;

		public FeatureService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public FeatureService()
		{
			_unitOfWork = new UnitOfWork();
		}

		public async Task<IEnumerable<FeatureFilm>> GetFeatures()
		{
			return await _unitOfWork.FeatureRepository.GetAll();
		}
	}
}
