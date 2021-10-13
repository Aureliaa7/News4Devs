using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using News4Devs.Core;
using News4Devs.Core.DomainServices;
using News4Devs.Core.Interfaces.Services;
using News4Devs.Core.Interfaces.UnitOfWork;
using News4Devs.Infrastructure.AppDbContext;
using News4Devs.Infrastructure.Services;
using News4Devs.Infrastructure.UnitOfWork;
using News4Devs.WebAPI.Filters;
using System.Text;

namespace News4Devs.WebAPI
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(
                  connectionString,
                  b => b.MigrationsAssembly(Constants.MigrationsAssembly))
            );
        }

        public static void ConfigureJwtAuthentication(this IServiceCollection services, string key)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public static void ConfigureGlobalFilters(this IServiceCollection services)
        {
            services.AddMvc(options => {
                options.Filters.Add(new AppExceptionFilter());
            });
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IJwtService, JwtService>();
        }
    }
}
