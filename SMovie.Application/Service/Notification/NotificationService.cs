using SMovie.Application.IService;
using SMovie.Domain.Models;
using SMovie.Domain.Repository;

namespace SMovie.Application.Service
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Notification> CreateNotification(string? methodType, string message, string avatar, string displayName)
        {
            var notification = new Notification
            {
                TypeMessage = methodType,
                Message = message,
                Avatar = avatar,
                DisplayName = displayName
            };

            await _unitOfWork.NotificationRepository.Add(notification);

            return notification;
        }

        public async Task<PagedList<Notification>> GetNotifications(int page, int pageSize)
        {
            var result = await _unitOfWork.NotificationRepository.GetAll(page, pageSize);
            return result;
        }
    }
}
