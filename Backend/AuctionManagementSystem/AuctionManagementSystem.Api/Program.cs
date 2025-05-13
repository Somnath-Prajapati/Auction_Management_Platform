
using AuctionManagementSystem.Api.Services;
using AuctionManagementSystem.Application.Contracts.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Application.Profiles;
using AuctionManagementSystem.Api.Middleware;
using ProtoBuf.Meta;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Api.Hubs;
using AuctionManagementSystem.Application.Contracts.RealTime;
using AuctionManagementSystem.Application;
using AuctionManagementSystem.Identity;
using AuctionManagementSystem.Persistence;

namespace AuctionManagementSystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddHttpContextAccessor();
            // Ensure the LoggedInUserService class implements the ILoggedInUserService interface correctly.
            //builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
            builder.Services.AddScoped<IBidNotificationService, BidNotificationService>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    // Add your frontend URL (e.g., localhost:5500 or file:// for testing locally)
                    policy.WithOrigins("http://localhost:5500", "http://localhost:4200", "https://localhost:4200", "file://")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials(); // Ensure cookies are sent (if needed)
                });
            });

            builder.Services.AddSignalR();

            var app = builder.Build();
            //if (app.Environment.IsDevelopment())
            //{
            app.UseCors("AllowFrontend");
            app.MapHub<BidHub>("/bidhub");
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Auction Management");
                });
            //}
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });


            app.MapControllers();
            app.Run();
        }
    }
}
