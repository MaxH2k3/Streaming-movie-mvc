using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SMovie.Application.IService;
using SMovie.Domain.Constants;
using SMovie.Domain.Entity;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;
using SMovie.WebUI.Constants;
using System.Net;
using System.Security.Claims;
using IAuthenticationService = SMovie.Application.IService.IAuthenticationService;

namespace SMovie.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly CookieSetting _cookieSetting;

        public LoginController(IUserService userService,
            IAuthenticationService authenticationService,
            IOptions<CookieSetting> cookieSetting)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _cookieSetting = cookieSetting.Value;
        }

        public IActionResult Index()
        {
            var token = Request.Cookies[UserClaimType.AccessToken];
            var role = User.FindFirstValue(UserClaimType.Role);

            if (!string.IsNullOrEmpty(token) && role.Equals(Role.RoleUser))
            {
                return Redirect(SystemDefault.UrlHome);
            } else if (!string.IsNullOrEmpty(token) && role.Equals(Role.RoleAdmin))
            {
                return Redirect(SystemDefault.UrlDashboard);
            }

            return View(ConstantView.Login);
        }

        public async Task<IActionResult> LoginAsync(UserDTO userDTO)
        {
            if(userDTO.UserName != null || userDTO.Password != null)
            {
                var response = await _userService.Login(userDTO);
                if (response.Status == HttpStatusCode.OK)
                {
                    var user = response.Data as User;

                    var claims = new List<Claim>
                    {
                        new(UserClaimType.UserId, user!.UserId.ToString()),
                        new(UserClaimType.Role, user.Role!),
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    //set information to cookie
                    var cookieOptions = new CookieOptions
                    {
                        Secure = _cookieSetting.IsPersistent,
                        HttpOnly = _cookieSetting.HttpOnly,
                        SameSite = (SameSiteMode)_cookieSetting.SameSite,
                        Expires = DateTime.Now.AddDays(_cookieSetting.ExpireTime)
                    };
                    Response.Cookies.Append(UserClaimType.DisplayName, user!.DisplayName!, cookieOptions);
                    Response.Cookies.Append(UserClaimType.Avatar, user.Avatar!, cookieOptions);

                    if(user.Role.Equals(UserRole.Admin.ToString()))
                    {
                        return Redirect(SystemDefault.UrlDashboard);
                    }

                    return Redirect(SystemDefault.UrlHome);
                }
                ViewBag.response = response;
            }
            
            return View(ConstantView.Login);
        }

        [HttpPost]
        public string LoginWithGoogleAsync(LoginWithDTO loginWithDTO)
        {

            //set information to cookie
            var cookieOptions = new CookieOptions
            {
                Secure = _cookieSetting.IsPersistent,
                HttpOnly = _cookieSetting.HttpOnly,
                SameSite = (SameSiteMode)_cookieSetting.SameSite,
                Expires = DateTime.Now.AddDays(_cookieSetting.ExpireTime)
            };
            Response.Cookies.Append(UserClaimType.DisplayName, loginWithDTO!.DisplayName!, cookieOptions);
            Response.Cookies.Append(UserClaimType.Avatar, loginWithDTO.Avatar!, cookieOptions);
            Response.Cookies.Append(UserClaimType.Account, AccountType.Google.ToString(), cookieOptions);

            return ConstantMessage.LoginSuccessfully;
        }

        [HttpPost]
        public string LoginWithMicrosoft(LoginWithDTO loginWithDTO)
        {
            //set information to cookie
            var cookieOptions = new CookieOptions
            {
                Secure = _cookieSetting.IsPersistent,
                HttpOnly = _cookieSetting.HttpOnly,
                SameSite = (SameSiteMode)_cookieSetting.SameSite,
                Expires = DateTime.Now.AddDays(_cookieSetting.ExpireTime)
            };

            Response.Cookies.Append(UserClaimType.DisplayName, loginWithDTO.DisplayName, cookieOptions);
            Response.Cookies.Append(UserClaimType.Avatar, loginWithDTO.Avatar, cookieOptions);
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
