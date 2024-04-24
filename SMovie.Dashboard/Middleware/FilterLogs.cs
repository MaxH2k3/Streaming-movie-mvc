using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SignalR;
using SMovie.Application.IService;
using SMovie.Dashboard.Hub;
using SMovie.Dashboard.Utilities;
using SMovie.Domain.Constants;

namespace SMovie.Dashboard.Middleware
{
    public class FilterLogs : IActionFilter
    {
        private readonly INotificationService _notificationService;
        private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;

        public FilterLogs(INotificationService notificationService,
                        IHubContext<NotificationHub, INotificationHub> hubContext)
        {
            _notificationService = notificationService;
            _hubContext = hubContext;
        }

        public async void OnActionExecuted(ActionExecutedContext context)
        {
            // Get the current action name
            var actionName = context.ActionDescriptor.RouteValues["action"];
            var tableName = Helper.GetTableUsingAction(actionName!);
            if (!string.IsNullOrEmpty(tableName))
            {
                var avatar = context.HttpContext.Request.Cookies["Avatar"];
                var displayName = context.HttpContext.Request.Cookies["DisplayName"];
                var message = $"has {actionName} in {tableName}";

                var notifcation = await _notificationService.CreateNotification(actionName, message, avatar!, displayName!);

                await _hubContext.Clients.All.SendNotification(notifcation);

            }

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
            
        }
    }
}
