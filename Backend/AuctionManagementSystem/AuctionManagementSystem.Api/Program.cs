using AuctionManagementSystem.Application;
using AuctionManagementSystem.Persistence;
using AuctionManagementSystem.Api.Services;
using AuctionManagementSystem.Application.Contracts.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using AuctionManagementSystem.Application.Contracts;

namespace AuctionManagementSystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAllOrigin", policy =>
            //    {
            //        policy.WithOrigins("http://localhost:4200") // Angular dev server
            //              .AllowAnyHeader()
            //              .AllowAnyMethod();
            //    });
            //});

            var app = builder.Build();
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Auction Manage");
                });
            //}
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseCors("AllowAngularDev"); //  CORS must come before authorization

            //app.UseStaticFiles();
            //app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
