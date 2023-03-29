using EduCRM.Application.Abstractions;
using EduCRM.Domain.Enums;
using EduCRM.Infrastructure.Persistence;
using EduCRM.Infrastructure.Providers;
using EduCRM.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EduCRM.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrostructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ITokenService, JWTService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddSingleton<IHashProvider, HashProvider>();
            services.AddScoped<IFileService, FileService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidIssuer = configuration["ValidIssuer"],
# pragma warning disable
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
#pragma warning restore
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminActions", policy =>
                {
                    policy.RequireClaim("Role", UserRole.Admin.ToString());
                });
            });
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            return services;
        }
    }
}