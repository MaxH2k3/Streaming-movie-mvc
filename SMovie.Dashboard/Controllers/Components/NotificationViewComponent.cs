using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;
using SMovie.Dashboard.Constants;

namespace SMovie.Dashboard.Controllers.Components
{
    [ViewComponent]
    public class NotificationViewComponent : ViewComponent
    {
        private readonly INotificationService _notificationService;

        public NotificationViewComponent(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notifications = await _notificationService.GetNotifications(SystemDefault.Page, 1);

            return View(notifications);
        }
    }
}
