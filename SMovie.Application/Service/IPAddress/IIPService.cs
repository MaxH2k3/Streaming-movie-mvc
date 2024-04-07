using SMovie.Domain.Models;

namespace SMovie.Application.IService
{
    public interface IIPService
    {
        Task<IEnumerable<BlackIP>> GetIPAddress();
        Task<ResponseDTO> BlockIpAddress(string ip);
        Task<ResponseDTO> UnblockIpAddress(string ip);
        Task<bool> IsBlocked(string ip);
    }
}
