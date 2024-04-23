using Microsoft.AspNetCore.Mvc.Filters;
using SMovie.Application.IService;
using SMovie.Dashboard.Utilities;
using SMovie.Domain.Constants;
using System;

namespace SMovie.Dashboard.Middleware
{
    public class FilterLogs : Attribute, IActionFilter
    {
        private readonly INotificationService _notificationService;

        public FilterLogs(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Get the current action name
            var actionName = context.ActionDescriptor.RouteValues["action"];
            if(!context.HttpContext.Request.Method.ToLower().Equals("get") && !actionName!.Equals("Login") && !actionName!.Equals("Auth"))
            {
                var displayName = context.HttpContext.Request.Cookies["DisplayName"];
                var userId = context.HttpContext.User.Claims.FirstOrDefault(u => u.Type.Equals(UserClaimType.UserId))?.Value;
                var methodType = Helper.GetMethodType(context.HttpContext.Request.Method);
                var message = $"{displayName} has {methodType} a record";

                _notificationService.CreateNotification(methodType, message, userId, actionName);
            }
        }
    }
}
