using MailKit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SMovie.Application.IService;
using SMovie.Application.Service;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;
using SMovie.Infrastructure.Repository;
using System.Text;

namespace SMovie.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllersWithViews();

            services.AddFluentEmail();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<Application.IService.IMailService, Application.Service.MailService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<INationService, NationService>();
            services.AddScoped<IMovieCategoryService, MovieCategoryService>();
            services.AddScoped<Application.IService.IAuthenticationService, Application.Service.AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<JWTSetting>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<SMovieSQLContext>();

            services.AddHttpContextAccessor();

            // Set up policies
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(UserRole.User.ToString(), policy => policy.RequireClaim("Role", UserRole.User.ToString()));
                opt.AddPolicy(UserRole.Admin.ToString(), policy => policy.RequireClaim("Role", UserRole.Admin.ToString()));
            });

            // set up JWT
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JWTSetting:Issuer"],
                    ValidAudience = configuration["JWTSetting:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSetting:Securitykey"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true
                };
            });

            return services;
        }
    }
}
