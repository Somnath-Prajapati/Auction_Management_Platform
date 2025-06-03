using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Persistence.Repositories;
using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Interfaces.Repositories;
using AuctionManagementSystem.Application.Repositories;
using AuctionManagementSystem.Domain.Interfaces;
using AuctionManagementSystem.Infrastructure.Persistence.Repositories;
using AuctionManagementSystem.Persistence.Context;
using AuctionManagementSystem.Persistence.Repositories.Assets;
using AuctionManagementSystem.Persistence.Repositories.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Meta;
using AuctionManagementSystem.Persistence.Repositories.User;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Infrastructure.UoW;
using AuctionManagementSystem.Application.Contracts.Request;
using AuctionManagementSystem.Persistence.Repositories.Requests;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Persistence.Repositories.Bids;
using AuctionManagementSystem.Persistence.Repositories.Listings;
using AuctionManagementSystem.Infrastructure.Repositories;
using AuctionManagementSystem.Application.Services;
using AuctionManagementSystem.Application.Contracts.Listings;
using AuctionManagementSystem.Application.Contracts.Chatbot;
using AuctionManagementSystem.Persistence.Repositories.Chatbot;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Persistence.Repositories.AuditTrail;
using AuctionManagementSystem.Persistence.Repositories.Transactions;
using AuctionManagementSystem.Application.Contracts.Roles;
using AuctionManagementSystem.Persistence.Repositories.Roles;
using AuctionManagementSystem.Application.Contracts.Notification;
using AuctionManagementSystem.Persistence.Repositories.Notification;
using AuctionManagementSystem.Persistence.Repositories.Transactions;
using AuctionManagementSystem.Application.Features.Transactions.Commands.CreateTransaction;
using AuctionManagementSystem.Persistence.Services;

namespace AuctionManagementSystem.Persistence
{

    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuctionManagementDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")).LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ISystemSettingsRepository, SystemSettingsRepository>();
            services.AddScoped<IDirectSaleSettingsRepository, DirectSaleSettingsRepository>();
            services.AddScoped<IFinanceSettingsRepository, FinanceSettingsRepository>();
            services.AddScoped<IFooterLinksSettingsRepository, FooterLinksSettingsRepository>();
            services.AddScoped<IStaticPagesSettingsRepository, StaticPagesSettingsRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ICardTypeRepository, CardTypeRepository>();
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();
            services.AddScoped<ITransactionStatusRepository, TransactionStatusRepository>();

            services.AddScoped<IAuctionRepository, AuctionRepository>();
            services.AddScoped<IAuctionUnitOfWork, AuctionUnitOfWork>();
            services.AddScoped<IAssetCategoriesRepository, AssetCategoriesRepository>();
            services.AddScoped<IAssetsRepository, AssetRepository>();
            services.AddScoped<IAssetGalleryRepository, AssetGalleryRepository>();
            services.AddScoped<IAssetDocumentRepository, AssetDocumentRepository>();
            services.AddScoped<IAssetDetailRepository, AssetDetailRepository>();
            services.AddScoped< IUserStatusRepository,UserStatusRepository> ();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IBidRepository, BidRepository>();
            services.AddScoped<IAuctionAssetRepository, AuctionAssetRepository >();
            services.AddScoped<IWishlistRepository, WishlistRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IAuctionAssetRepository, AuctionAssetRepository>();
            services.AddScoped<IAuctionWinnerService, AuctionWinnerService>();
            services.AddScoped<IAssetWinnerRepository, AssetWinnerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IChatbotRepository, ChatbotRepository>();
            services.AddScoped<IOrderEmailService, OrderEmailService>();
            services.AddScoped<INotificationRepository, NotificationRepository>();


            services.AddScoped<IAutoBidRepository, AutoBidRepository>();


            services.AddScoped<IAssetExpirationService, AssetExpirationService>();
            
            services.AddScoped<IAuditTrailRepository, AuditTrailRepository>();
            services.AddScoped<IRolePermissionsMatrixRepository, RolePermissionsMatrixRepository>();
            services.AddScoped<IAutoRefundService, AutoRefundService>();

            services.AddHostedService<AutoRefundHostedService>();
            services.AddHostedService<ExpireCartItemsHostedService>();


            services.AddScoped<IUserDepositRepository, UserDepositRepository>();
            services.AddScoped<ProcessAutoRefundHandler>();



            return services;
        }
    }

}
