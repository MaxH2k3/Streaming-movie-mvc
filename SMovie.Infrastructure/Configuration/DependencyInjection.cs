using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SMovie.Application.IService;
using SMovie.Application.Service;
using SMovie.Domain.Constants;
using SMovie.Domain.Models;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;
using SMovie.Infrastructure.Repository;

namespace SMovie.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this WebApplicationBuilder builder)
        {
            // Set up FluentEmail
            builder.Services.AddFluentEmail(builder.Configuration);

            // Set up cookie authen
            builder.Services.Configure<CookieSetting>(builder.Configuration.GetSection("CookieSetting"));
            builder.Services.AddCustomCookie(builder.Configuration);

            // Set up AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Set up builder.Services
            builder.Services.AddScoped<IMailService, MailService>();
            builder.Services.AddScoped<IPersonService, PersonService>();
            builder.Services.AddScoped<ICommonService, CommonService>();
            builder.Services.AddScoped<IMovieCategoryService, MovieCategoryService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<ICastService, CastService>();
            builder.Services.AddScoped<IIPService, IPService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Set up Context
            builder.Services.AddDbContext<SMovieSQLContext>();

            // Set up repositories
            builder.Services.AddHttpContextAccessor();

            // Set up cors
            builder.Services.AddCors();

            // Set up policies
            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy("User", policy => policy.RequireClaim(UserClaimType.Role, Role.RoleUser));
                opt.AddPolicy("Admin", policy => policy.RequireClaim(UserClaimType.Role, Role.RoleAdmin));
            });
        }
    }
}
