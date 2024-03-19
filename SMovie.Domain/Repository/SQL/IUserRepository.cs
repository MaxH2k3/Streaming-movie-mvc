using SMovie.Domain.Entity;
using SMovie.Domain.Enum;

namespace SMovie.Domain.Repository
{
	public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUser(string username);
        Task<bool> IsExisted(UserFieldType field, string value);
    }
}
