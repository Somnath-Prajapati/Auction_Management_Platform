using System.Reflection;
using FluentValidation;
using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.CreateFinanceSettings;
using AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.UpdateFinanceSettings;
using AuctionManagementSystem.Application.Validators;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Command.CreateStaticPagesSettings;
using AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Command.UpdateStaticPagesSettings;
using AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Command.DeleteStaticPagesSettings;
using AuctionManagementSystem.Application.Profiles;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Application.Services;

namespace AuctionManagementSystem.Application
{
    public static class ApplicationServicesRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(MapperProfile).Assembly);


            // Add FluentValidation
            services.AddValidatorsFromAssemblyContaining<CreateStaticPagesSettingsCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateStaticPagesSettingsCommandValidator>();

            // Add MediatR handlers
            services.AddTransient<IValidator<FinanceSettingsDto>, FinanceSettingsDtoValidator>();
            services.AddTransient<IValidator<FinanceSettingsDto>, CreateFinanceSettingsCommandValidator>();
            services.AddTransient<IRequestHandler<UpdateFinanceSettingsCommand, FinanceSettingsDto>, UpdateFinanceSettingsCommandHandler>();

            // Add Static Pages settings validators
            services.AddTransient<IValidator<StaticPagesSettingsDto>, CreateStaticPagesSettingsCommandValidator>();
            services.AddTransient<IValidator<UpdateStaticPagesSettingsCommand>, UpdateStaticPagesSettingsCommandValidator>(); // Fix: Corrected the type parameter to match the validator's target type
            services.AddValidatorsFromAssemblyContaining<FooterLinksSettingsDtoValidator>();
            services.AddScoped<IAuctionJobScheduler, HangfireAuctionJobScheduler>();

            services.AddScoped<ChatBotService>();

            services.AddScoped<IAutoBidService, AutoBidService>();

            services.AddTransient<HangfireAutoBidJobScheduler>();


            // Add pipeline behavior for validation
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddHttpContextAccessor();
            return services;    
        }
    }
}
 