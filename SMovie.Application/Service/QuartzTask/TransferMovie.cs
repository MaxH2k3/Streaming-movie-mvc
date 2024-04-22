
using Quartz;
using SMovie.Domain.Repository;


namespace SMovie.Application.Service.QuartzTask
{
    [DisallowConcurrentExecution]
    public class TransferMovie : IJob
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransferMovie(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            // Do something
            await _unitOfWork.CurrentTopMovieRepository.ConvertToPrevious();
        }
    }
}
