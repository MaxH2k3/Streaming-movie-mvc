using SMovie.Application.Helper;
using SMovie.Application.IService;
using SMovie.Domain.Enum;
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

        public async Task CreateNotification(MethodType methodType, string message, string? userId, string action)
        {
            var notification = new Notification
            {
                TypeMessage = methodType,
                Message = message + " in " + ActionHelper.GetTableUsingAction(action),
                UserId = string.IsNullOrEmpty(userId) ? null : Guid.Parse(userId),
            };

            await _unitOfWork.NotificationRepository.Add(notification);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
