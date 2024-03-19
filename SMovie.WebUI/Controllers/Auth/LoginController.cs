using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SMovie.Application.IService;
using SMovie.Domain.Entity;
using SMovie.Domain.Models;
using System.Net;

namespace SMovie.WebUI.Controllers
{

    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IUserService userService,
            IAuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }

        public IActionResult Index()
        {
            var token = Request.Cookies["AccessToken"];

            if (!string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Home");
            }

            return View("../Auth/Login");
        }

        public async Task<IActionResult> LoginAsync(UserDTO userDTO)
        {
            if(userDTO.UserName != null && userDTO.Password != null)
            {
                var response = await _userService.Login(userDTO);
                if (response.Status == HttpStatusCode.OK)
                {
                    var user = response.Data as User;

                    CookieOptions cookieOptions = new CookieOptions
                    {
                        // Set the secure flag, which Chrome's changes will require for SameSite none.
                        // Note this will also require you to be running on HTTPS.
                        Secure = true,

                        // Set the cookie to HTTP only which is good practice unless you really do need
                        // to access it client side in scripts.
                        HttpOnly = true,

                        // Add the SameSite attribute, this will emit the attribute with a value of none.
                        SameSite = SameSiteMode.None,

                        // The client should follow its default cookie policy.
                        // SameSite = SameSiteMode.Unspecified

                        Expires = DateTime.Now.AddMonths(1)

                    };

                    //set information to cookie
                    Response.Cookies.Append("DisplayName", user!.DisplayName!, cookieOptions);
                    Response.Cookies.Append("Avatar", user.Avatar!, cookieOptions);
                    Response.Cookies.Append("AccessToken", await _authenticationService.GenerateToken(userDTO), cookieOptions);

                    return RedirectToAction("Index", "Home");
                }
                ViewBag.response = response;
            }
            
            return View("../Auth/Login");
        }

        [HttpPost]
        public string LoginWithGoogleAsync(LoginWithDTO loginWithDTO)
        {
            CookieOptions cookieOptions = new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.None,

                Expires = DateTime.Now.AddMonths(1)

            };

            //set information to cookie
            Response.Cookies.Append("DisplayName", loginWithDTO.DisplayName, cookieOptions);
            Response.Cookies.Append("Avatar", loginWithDTO.Avatar, cookieOptions);
            Response.Cookies.Append("AccessToken", loginWithDTO.AccessToken, cookieOptions);

            return "Login Successfully!";
        }

        [HttpPost]
        public string LoginWithMicrosoft(LoginWithDTO loginWithDTO)
        {
            CookieOptions cookieOptions = new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.None,

                Expires = DateTime.Now.AddMonths(1)

            };

            //set information to cookie
            Response.Cookies.Append("DisplayName", loginWithDTO.DisplayName, cookieOptions);
            Response.Cookies.Append("Avatar", loginWithDTO.Avatar, cookieOptions);
            Response.Cookies.Append("AccessToken", loginWithDTO.AccessToken, cookieOptions);

            return "Login Successfully!";
        }

        public IActionResult ForgotPassword()
        {
            return View("../Auth/ForgotPassword");
        }

        public IActionResult Logout()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            TempData["Response"] = JsonConvert.SerializeObject(new { Status = HttpStatusCode.OK, Message = "Logout Successfully!" });

            return RedirectToAction("Index", "Home");
        }

    }
}
