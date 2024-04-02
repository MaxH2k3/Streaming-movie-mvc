using SMovie.Application.IService;
using SMovie.Domain.Entity;
using SMovie.Domain.Repository;

namespace SMovie.Application.Service
{
    public class NationService : INationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Nation>> GetNations()
        {
            return await _unitOfWork.NationRepository.GetAll();
        }
    }
}
