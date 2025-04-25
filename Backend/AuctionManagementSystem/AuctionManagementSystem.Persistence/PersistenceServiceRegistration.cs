using AuctionManagementSystem.Persistence.Repositories;
using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Interfaces.Repositories;
using AuctionManagementSystem.Application.Repositories;
using AuctionManagementSystem.Domain.Interfaces;
using AuctionManagementSystem.Infrastructure.Persistence.Repositories;
using AuctionManagementSystem.Persistence.Context;
using AuctionManagementSystem.Persistence.Repositories.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuctionManagementSystem.Persistence.Repositories.User;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Infrastructure.UoW;

namespace AuctionManagementSystem.Persistence
{

    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuctionManagementDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
             services.AddScoped<ISystemSettingsRepository, SystemSettingsRepository>();
            services.AddScoped<IDirectSaleSettingsRepository, DirectSaleSettingsRepository>();
            services.AddScoped<IFinanceSettingsRepository, FinanceSettingsRepository>();
            services.AddScoped<IFooterLinksSettingsRepository, FooterLinksSettingsRepository>();
            services.AddScoped<IStaticPagesSettingsRepository, StaticPagesSettingsRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IAuctionRepository, AuctionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }
    }

}
