using SMovie.Domain.Models;

namespace SMovie.Application.IService;

public interface IAuthenticationService
{
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
}
