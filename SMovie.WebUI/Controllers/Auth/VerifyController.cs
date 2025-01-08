using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;
using SMovie.Domain.Enum;
using System.Net;

namespace SMovie.WebUI.Controllers.Auth
{
    public class VerifyController : Controller
    {
        private readonly IUserService _userService;

        public VerifyController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var token = Request.Cookies["AccessToken"];

            if (!string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Home");
            } else if(string.IsNullOrEmpty(TempData["UserId"]!.ToString()))
            {
                return RedirectToAction("Index", "Register");
            }

            ViewBag.UserId = TempData["UserId"];

            return View("../Auth/ConfirmCode");
        }

        public async Task<IActionResult> VerifyCode(int code, Guid userId)
        {
            var response = await _userService.VerifyAccount(code.ToString(), userId, VerifyType.Code);
            if (response.Status == HttpStatusCode.OK)
            {
                return RedirectToAction("Index", "Login");
            }

            return View("../Auth/ConfirmCode");
        }

        public async Task<IActionResult> VerifyToken(string token, Guid userId)
        {
            var response = await _userService.VerifyAccount(token, userId, VerifyType.Token);
            if (response.Status == HttpStatusCode.OK)
            {
                return RedirectToAction("Index", "Login");
            }

            return View("../Auth/ConfirmCode");
        }

    }
}
