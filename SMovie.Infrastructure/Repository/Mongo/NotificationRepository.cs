using SMovie.Domain.Models;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;

namespace SMovie.Infrastructure.Repository
{
    public class NotificationRepository : MongoRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(SMovieMongoContext context) : base(context.Notifications)
        {
        }
    }
}
