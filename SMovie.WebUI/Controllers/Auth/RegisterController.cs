using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;
using SMovie.Domain.Models;
using SMovie.WebUI.Constants;
using System.Net;

namespace Streamit_movie_mvc.Controllers.Main
{
    public class RegisterController : Controller
    {
        private readonly IUserService _userService;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var token = Request.Cookies["AccessToken"];

            if (!string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Home");
            }

            return View(ConstantView.Register);
        }

        public async Task<IActionResult> Register(RegisterUser registerUser)
        {
            ResponseDTO response = await _userService.Register(registerUser);

            if(response.Status == HttpStatusCode.Created)
            {
                TempData["UserId"] = response.Data;
                return RedirectToAction("Index", "Verify", response.Data);
            }

            ViewBag.response = response;

            return View(ConstantView.Register, registerUser);
        }

        public IActionResult ConfirmCode()
        {
            ViewBag.UserId = "aaaa";
            return View(ConstantView.ConfirmCode);
        }
    }
}
