using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AuctionManagementSystem.Persistence.Repositories.Listings
{
    public class ExpireCartItemsHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ExpireCartItemsHostedService> _logger;

        public ExpireCartItemsHostedService(IServiceScopeFactory scopeFactory, ILogger<ExpireCartItemsHostedService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Cart Expiry Hosted Service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AuctionManagementDbContext>();
                    
                    var now = DateTime.UtcNow;
                    //var expiredCartItems = dbContext.TblCartItems
                    //    .Where(x => x.IsExpired == false && x.ExpiryTime <= now)
                    //    .ToList();

                    //foreach (var item in expiredCartItems)
                    //{
                    //    item.IsExpired = true;
                    //    // or item.IsDeleted = true;
                    //}

                    await dbContext.SaveChangesAsync(stoppingToken);
                }

                // Wait before the next check
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }

}
