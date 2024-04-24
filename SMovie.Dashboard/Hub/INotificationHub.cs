using SMovie.Domain.Models;

namespace SMovie.Dashboard.Hub
{
    public interface INotificationHub
    {
        Task SendNotification(Notification notification);
        Task SendTest(string str);
    }
}
