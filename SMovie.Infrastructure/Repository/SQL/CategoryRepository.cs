using SMovie.Domain.Entity;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;
using SMovie.Infrastructure.Repository.Common;

namespace SMovie.Infrastructure.Repository
{
    public class CategoryRepository : SQLExtendRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(SMovieSQLContext context) : base(context)
        {
        }
    }
}
