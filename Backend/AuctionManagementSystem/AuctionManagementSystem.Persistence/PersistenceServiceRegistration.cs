using AuctionManagementSystem.Application.Contracts.Request;
using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Interfaces.Repositories;
using AuctionManagementSystem.Application.Repositories;
using AuctionManagementSystem.Domain.Interfaces;
using AuctionManagementSystem.Infrastructure.Persistence.Repositories;
using AuctionManagementSystem.Persistence.Context;
using AuctionManagementSystem.Persistence.Repositories;
using AuctionManagementSystem.Persistence.Repositories.Requests;
using AuctionManagementSystem.Persistence.Repositories.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Meta;

namespace AuctionManagementSystem.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register your DbContext and other persistence-related services here
            services.AddDbContext<AuctionManagementDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            // Register repositories
            services.AddScoped<ISystemSettingsRepository, SystemSettingsRepository>();
            services.AddScoped<IDirectSaleSettingsRepository, DirectSaleSettingsRepository>();
            services.AddScoped<IFinanceSettingsRepository, FinanceSettingsRepository>();
            services.AddScoped<IFooterLinksSettingsRepository, FooterLinksSettingsRepository>();
            services.AddScoped<IStaticPagesSettingsRepository, StaticPagesSettingsRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
           services.AddScoped<IRequestRepository, RequestRepository>();


            return services;
        }
    }

}
