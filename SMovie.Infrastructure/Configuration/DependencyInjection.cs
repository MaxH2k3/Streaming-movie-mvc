using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMovie.Application.IService;
using SMovie.Application.Service;
using SMovie.Domain.Constants;
using SMovie.Domain.Models;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;
using SMovie.Infrastructure.Repository;
using System.Data;

namespace SMovie.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationManager configuration)
        {
            // Set up FluentEmail
            services.AddFluentEmail(configuration);

            // Set up cookie authen
            services.Configure<CookieSetting>(configuration.GetSection("CookieSetting"));
            services.AddCustomCookie(configuration);

            // Set up AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Set up services
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IMovieCategoryService, MovieCategoryService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ICastService, CastService>();
            services.AddScoped<IIPService, IPService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Set up Context
            services.AddDbContext<SMovieSQLContext>();

            // Set up repositories
            services.AddHttpContextAccessor();

            services.AddCors();

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("User", policy => policy.RequireClaim(UserClaimType.Role, Role.RoleUser));
                opt.AddPolicy("Admin", policy => policy.RequireClaim(UserClaimType.Role, Role.RoleAdmin));
            });

            return services;
        }
    }
}
