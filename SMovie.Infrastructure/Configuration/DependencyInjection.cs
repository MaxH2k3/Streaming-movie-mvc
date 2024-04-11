using Microsoft.Extensions.DependencyInjection;
using SMovie.Application.IService;
using SMovie.Application.Service;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;
using SMovie.Infrastructure.Repository;

namespace SMovie.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllersWithViews();

            // Set up FluentEmail
            services.AddFluentEmail();

            // Set up AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Set up services
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<INationService, NationService>();
            services.AddScoped<IMovieCategoryService, MovieCategoryService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IIPService, IPService>();
            services.AddScoped<JWTSetting>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Set up Context
            services.AddDbContext<SMovieSQLContext>();

            // Set up repositories
            services.AddHttpContextAccessor();

            // Set up policies
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(UserRole.User.ToString(), policy => policy.RequireClaim("Role", UserRole.User.ToString()));
                opt.AddPolicy(UserRole.Admin.ToString(), policy => policy.RequireClaim("Role", UserRole.Admin.ToString()));
            });

            // Set up JWT
            services.AddJwt();

            return services;
        }
    }
}
