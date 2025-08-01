
using AuctionManagementSystem.Api.Hubs;
using AuctionManagementSystem.Api.Middleware;
using AuctionManagementSystem.Api.Services;
using AuctionManagementSystem.Application;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Application.Contracts.Chatbot;
using AuctionManagementSystem.Application.Contracts.FAQ;
using AuctionManagementSystem.Application.Contracts.Notification;
using AuctionManagementSystem.Application.Contracts.RealTime;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Services;
using AuctionManagementSystem.Identity;
using AuctionManagementSystem.Persistence;
using AuctionManagementSystem.Persistence.Repositories.Chatbot;
using AuctionManagementSystem.Persistence.Repositories.FAQs;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using Stripe;
using FileService = AuctionManagementSystem.Api.Services.FileService;

namespace AuctionManagementSystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //Log.Logger = new LoggerConfiguration()
            //   .MinimumLevel.Debug()
            //   .WriteTo.File("Logs/hangfire-errors.txt", rollingInterval: RollingInterval.Day)
            //   .CreateLogger();


            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();

            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
            builder.Services.AddScoped<IBidNotificationService, BidNotificationService>();
            builder.Services.AddScoped<IWinnerNotificationService, WinnerNotificationService>();
            //builder.Services.AddScoped<IBackgroundProcess, BackgroundProcessHangfire>();


            //new added
            builder.Services.AddScoped<IChatbotRepository, ChatbotRepository>();
            builder.Services.AddScoped<ChatBotService>();

            builder.Services.AddScoped<IGetAllFAQ, FAQsRepository>();


            builder.Services.AddScoped<HangfireAutoBidJobScheduler>();




            builder.Services.AddScoped<INotificationBroadcaster, NotificationBroadcaster>();
            builder.Services.AddSingleton<IUserIdProvider, NameIdentifierUserIdProvider>();

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

            //builder.Services.AddHangfire(config =>
            //    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"), new Hangfire.SqlServer.SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true
                });

            });

            builder.Services.AddHangfireServer();

            GlobalJobFilters.Filters.Add(new LogHangfireJobExceptionFilter());

            builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);


            var app = builder.Build();

            app.Lifetime.ApplicationStarted.Register(() =>
            {
                using var scope = app.Services.CreateScope();
                var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
                recurringJobManager.RemoveIfExists("AutoBidJob");

                var scheduler = scope.ServiceProvider.GetRequiredService<HangfireAutoBidJobScheduler>();
                scheduler.ScheduleAutoBidJob();
            });




            app.UseCors("AllowFrontend");

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                //});
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Auction Management");
            });
            //}
            app.UseMiddleware<ExceptionMiddleware>();


            app.UseHttpsRedirection();


            app.UseStaticFiles();
            app.UseHangfireDashboard();






            StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:Secret_key").Get<String>();


            app.UseAuthentication();

            app.UseAuthorization();
            app.MapHub<BidHub>("/bidhub");





            app.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });


            app.MapControllers();
            app.Run();
        }
    }


    public class LogHangfireJobExceptionFilter : JobFilterAttribute, IApplyStateFilter
    {
        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            if (context.NewState is FailedState failedState)
            {
                Log.Error(failedState.Exception,
                    $"Hangfire Job {context.BackgroundJob.Id} failed: {failedState.Exception.Message}");
            }
        }

        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction) { }
    }
}

