
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
using Hangfire;
using Stripe;
using FileService = AuctionManagementSystem.Api.Services.FileService;
using AuctionManagementSystem.Application.Contracts.Chatbot;
using AuctionManagementSystem.Application.Services;
using AuctionManagementSystem.Persistence.Repositories.Chatbot;
using AuctionManagementSystem.Persistence.Context;

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
            builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
            builder.Services.AddScoped<IBidNotificationService, BidNotificationService>();
            builder.Services.AddScoped<IWinnerNotificationService, WinnerNotificationService>();

            //new added
            builder.Services.AddScoped<IChatbotRepository, ChatbotRepository>();
            builder.Services.AddScoped<ChatBotService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:5500", "http://localhost:4200", "https://localhost:4200", "file://")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials(); 
                });
            });

            builder.Services.AddSignalR();

            builder.Services.AddHangfire(config =>
                config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddHangfireServer();
            builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);


            var app = builder.Build();
            //if (app.Environment.IsDevelopment())
            //{
            
            app.UseCors("AllowFrontend");

            app.MapHub<BidHub>("/bidhub");
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
            //});
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Auction Management");
                });
            //}
            app.UseMiddleware<ExceptionMiddleware>();
            
            
            app.UseHttpsRedirection();




            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //    Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "AssetGallery")),
            //    RequestPath = "/AssetGallery"
            //});

            

            app.UseStaticFiles();
            app.UseHangfireDashboard();




          

            StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:Secret_key").Get<String>();

            
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
