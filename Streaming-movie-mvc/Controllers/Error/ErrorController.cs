using Microsoft.AspNetCore.Mvc;

namespace SMovie.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HandleException(int statusCode)
        {
            if(statusCode == 404)
                return View("NotFound");

            return View("ServerError");
        }
    }
}
