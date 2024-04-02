using Microsoft.EntityFrameworkCore;
using SMovie.Domain.Entity;
using SMovie.Domain.Enum;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;
using SMovie.Infrastructure.Repository.Common;

namespace SMovie.Infrastructure.Repository
{
    public class UserRepository : SQLExtendRepository<User>, IUserRepository
    {
        private readonly SMovieSQLContext _context;

        public UserRepository(SMovieSQLContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetUser(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username!.Equals(username) || x.Email!.Equals(username));
        }

        public async Task<bool> IsExisted(UserFieldType field, string value)
        {
            if (UserFieldType.Username == field)
            {
                return (await GetAll()).Any(u => u.Username!.ToLower().Equals(value.ToLower()));
            } else if (UserFieldType.Email == field)
            {
                return (await GetAll()).Any(u => u.Email!.ToLower().Equals(value.ToLower()));
            }

            return false;
        }

    }
}
