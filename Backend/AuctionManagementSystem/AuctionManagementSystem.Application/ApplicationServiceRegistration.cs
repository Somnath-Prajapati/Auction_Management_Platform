
using System.Reflection;
using FluentValidation;
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
            return services;
        }
    }
}
