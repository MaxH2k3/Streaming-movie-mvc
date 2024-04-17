

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMovie.Domain.Models;

namespace SMovie.Infrastructure.Configuration;

public static class CookieConfig
{
    public static void AddCustomCookie(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                var cookieSetting = configuration.GetSection("CookieSetting").Get<CookieSetting>();

                options.ExpireTimeSpan = TimeSpan.FromDays(cookieSetting.ExpireTime);
                options.Cookie.HttpOnly = cookieSetting.HttpOnly;
                options.Cookie.SecurePolicy = (CookieSecurePolicy)cookieSetting.SecurePolicy;
                options.Cookie.SameSite = (SameSiteMode)cookieSetting.SameSite;
                options.Cookie.Name = cookieSetting.Name;
            });
    }
}
