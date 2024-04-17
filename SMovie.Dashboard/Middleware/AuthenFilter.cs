using Microsoft.AspNetCore.Mvc.Filters;
using Org.BouncyCastle.Asn1.Ocsp;
using SMovie.Dashboard.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SMovie.Dashboard.Middleware;

public class AuthenFilter : IAuthorizationFilter
{

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        try
        {
            var token = context.HttpContext.Request.Cookies[UserClaimType.AccessToken];

            if (!string.IsNullOrEmpty(token) && !context.HttpContext.User.Claims.Any())
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                var claims = jwtToken.Claims;

                var claimsIdentity = new ClaimsIdentity(claims);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                context.HttpContext.User = claimsPrincipal;
            } else if (string.IsNullOrEmpty(token))
            {
                context.HttpContext.Response.Redirect("https://localhost:7066/login");
            }

        } catch
        {
            foreach (var cookie in context.HttpContext.Request.Cookies.Keys)
            {
                context.HttpContext.Response.Cookies.Delete(cookie);
            }

            context.HttpContext.Response.Redirect("https://localhost:7066/login");
        }
    }
}
