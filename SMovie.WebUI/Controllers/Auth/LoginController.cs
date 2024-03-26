using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SMovie.Application.IService;
using SMovie.Domain.Entity;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;
using SMovie.WebUI.Constants;
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
            var token = Request.Cookies[UserClaimType.AccessToken];

            if (!string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Home");
            }

            return View(ConstantView.Login);
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
                    Response.Cookies.Append(UserClaimType.DisplayName, user!.DisplayName!, cookieOptions);
                    Response.Cookies.Append(UserClaimType.Avatar, user.Avatar!, cookieOptions);
                    Response.Cookies.Append(UserClaimType.AccessToken, await _authenticationService.GenerateToken(userDTO), cookieOptions);
                    Response.Cookies.Append(UserClaimType.Account, AccountType.System.ToString(), cookieOptions);

                    return RedirectToAction("Index", "Home");
                }
                ViewBag.response = response;
            }
            
            return View(ConstantView.Login);
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
            Response.Cookies.Append(UserClaimType.DisplayName, loginWithDTO.DisplayName, cookieOptions);
            Response.Cookies.Append(UserClaimType.Avatar, loginWithDTO.Avatar, cookieOptions);
            Response.Cookies.Append(UserClaimType.AccessToken, loginWithDTO.AccessToken, cookieOptions);
            Response.Cookies.Append(UserClaimType.Account, AccountType.Google.ToString(), cookieOptions);

            return ConstantMessage.LoginSuccessfully;
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
            Response.Cookies.Append(UserClaimType.DisplayName, loginWithDTO.DisplayName, cookieOptions);
            Response.Cookies.Append(UserClaimType.Avatar, loginWithDTO.Avatar, cookieOptions);
            Response.Cookies.Append(UserClaimType.AccessToken, loginWithDTO.AccessToken, cookieOptions);
            Response.Cookies.Append(UserClaimType.Account, AccountType.Microsoft.ToString(), cookieOptions);

            return ConstantMessage.LoginSuccessfully;
        }

        public IActionResult ForgotPassword()
        {
            return View(ConstantView.ForgotPassword);
        }

        public IActionResult Logout()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            TempData["Response"] = JsonConvert.SerializeObject(new { Status = HttpStatusCode.OK, Message = ConstantMessage.LogoutSuccessfully });

            return RedirectToAction("Index", "Home");
        }

    }
}
