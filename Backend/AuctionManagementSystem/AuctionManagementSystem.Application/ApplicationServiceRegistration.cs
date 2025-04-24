using System.Reflection;
using FluentValidation;
using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.CreateFinanceSettings;
using AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.UpdateFinanceSettings;
using AuctionManagementSystem.Application.Validators;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using AuctionManagementSystem.Application.Services;
namespace AuctionManagementSystem.Application
{
    public static class ApplicationServicesRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddHttpContextAccessor();
             services.AddTransient<IValidator<FinanceSettingsDto>, UpdateFinanceSettingsCommandValidator>();
            services.AddTransient<IValidator<FinanceSettingsDto>, CreateFinanceSettingsCommandValidator>();

            return services;
        }
    }
}
