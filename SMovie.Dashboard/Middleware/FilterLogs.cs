using Microsoft.AspNetCore.Mvc.Filters;

namespace SMovie.Dashboard.Middleware
{
    public class FilterLogs : Attribute, IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Action executed");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.HttpContext.Request.Method.ToLower().Equals("get"))
            {
                Console.WriteLine("Request method is: " + context.HttpContext.Request.Method);
            }
        }
    }
}
