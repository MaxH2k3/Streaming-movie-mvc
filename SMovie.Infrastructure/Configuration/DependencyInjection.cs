using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMovie.Application.IService;
using SMovie.Application.Service;
using SMovie.Domain.Models;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;
using SMovie.Infrastructure.Repository;

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
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<INationService, NationService>();
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

            return services;
        }
    }
}
