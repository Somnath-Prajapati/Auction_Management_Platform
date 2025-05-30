using AuctionManagementSystem.Application.Contracts.Auth;
using IUnitOfWorkAuth = AuctionManagementSystem.Application.Contracts.Auth.IUnitOfWorkAuth;
using AuctionManagementSystem.Identity.Repository;
using Microsoft.Extensions.DependencyInjection;
using AuctionManagementSystem.Identity.Services;
using AuctionManagementSystem.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AuctionManagementSystem.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
//using AuctionManagementSystem.Persistence.Services;

namespace AuctionManagementSystem.Identity
{
    public static class IdentityServiceRegistration
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IAuditJobScheduler, AuditJobScheduler>();

            services.AddScoped<IOtpRepository, OtpRepository>();
            services.AddScoped<IUnitOfWorkAuth, UnitOfWorkAuth>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuditTrailService, AuditTrailService>();
            //new added
            services.AddScoped<IPasswordHasher<TblUser>, PasswordHasher<TblUser>>();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = jwtSettings.Issuer,
                     ValidAudience = jwtSettings.Audience,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                 };

                 // Important for SignalR JWT authentication
                 options.Events = new JwtBearerEvents
                 {
                     OnMessageReceived = context =>
                     {
                         var accessToken = context.Request.Query["access_token"];
                         var path = context.HttpContext.Request.Path;
                         if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/bidhub"))
                         {
                             context.Token = accessToken;
                         }
                         return Task.CompletedTask;
                     }
                 };
             });


            return services;
        }
    }
}
