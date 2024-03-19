using SMovie.Application.IService;
using SMovie.Domain.UnitOfWork;
using SMovie.Domain.Entity;
using SMovie.Infrastructure.UnitOfWork;

namespace SMovie.Application.Service
{
    public class NationService : INationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public NationService()
        {
            _unitOfWork = new UnitOfWork();
		}

        public async Task<IEnumerable<Nation>> GetNations()
        {
            return await _unitOfWork.NationRepository.GetAll();
        }
    }
}
