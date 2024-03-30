

using SMovie.Domain.Constants;
using SMovie.Domain.Entity;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;

namespace SMovie.Application.IService
{
    public interface IUserService
    {
        Task<User?> GetUser(Guid userId);
        Task<IEnumerable<User>> GetUsers();
        Task<ResponseDTO> Register(RegisterUser registerUser);
        Task<ResponseDTO> Login(UserDTO userDTO);
        Task<ResponseDTO> VerifyAccount(string token, Guid userId, VerifyType type);
        Task<ResponseDTO> ResendToken(Guid userId);
        Task<PagedList<User>> GetUserByStatus(AccountStatus status, int page, int eachPage);
    }
}
