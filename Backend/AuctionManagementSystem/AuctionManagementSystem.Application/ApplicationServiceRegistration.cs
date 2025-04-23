using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.CreateFinanceSettings;
using AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.UpdateFinanceSettings;
namespace AuctionManagementSystem.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register all MediatR handlers in the current assembly
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Register AutoMapper profiles from this assembly
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Register FluentValidation validators
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // Fix for CS1061

            services.AddTransient<IValidator<FinanceSettingsDto>, UpdateFinanceSettingsCommandValidator>();
            services.AddTransient<IValidator<FinanceSettingsDto>, CreateFinanceSettingsCommandValidator>();


            return services;
        }
    }
}
