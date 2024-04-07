using SMovie.Application.IService;
using SMovie.Application.MessageService;
using SMovie.Domain.Models;
using SMovie.Domain.Repository;
using System.Net;

namespace SMovie.Application.Service
{
    public class IPService : IIPService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IPService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> BlockIpAddress(string ip)
        {
            if(await IsBlocked(ip))
                return new ResponseDTO
                {
                    Message = MessageSystem.IPAlreadyBlocked,
                    Status = HttpStatusCode.BadRequest
                };

            await _unitOfWork.BlackIPRepository.Add(new BlackIP
            {
                IP = IPAddress.Parse(ip),
                DateCreated = DateTime.Now
            });

            return new ResponseDTO
            {
                Message = MessageSystem.BlockedIP,
                Status = HttpStatusCode.Created
            };
        }

        public async Task<IEnumerable<BlackIP>> GetIPAddress()
        {
            return await _unitOfWork.BlackIPRepository.GetAll();
        }

        public async Task<bool> IsBlocked(string ip)
        {
            _ = IPAddress.TryParse(ip, out var ipAddress);
            var result = await _unitOfWork.BlackIPRepository.GetById(ipAddress!);
            return result != null;
        }

        public async Task<ResponseDTO> UnblockIpAddress(string ip)
        {
            await _unitOfWork.BlackIPRepository.Delete(IPAddress.Parse(ip));

            return new ResponseDTO
            {
                Message = MessageSystem.UnblockedIP,
                Status = HttpStatusCode.OK
            };
        }
    }
}
