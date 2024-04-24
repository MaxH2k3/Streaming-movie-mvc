using SMovie.Domain.Models;

namespace SMovie.Application.IService
{
    public interface INotificationService
    {
        Task<Notification> CreateNotification(string? methodType, string message, string avatar, string displayName);
        Task<PagedList<Notification>> GetNotifications(int page, int pageSize);
    }
}
