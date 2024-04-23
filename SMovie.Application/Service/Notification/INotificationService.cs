using SMovie.Domain.Enum;

namespace SMovie.Application.IService
{
    public interface INotificationService
    {
        Task CreateNotification(MethodType methodType, string message, string? userId, string action);
    }
}
