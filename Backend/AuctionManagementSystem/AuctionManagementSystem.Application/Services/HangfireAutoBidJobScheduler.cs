using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Bids;
using Hangfire;

namespace AuctionManagementSystem.Application.Services
{
    public class HangfireAutoBidJobScheduler : IAutoBidJobScheduler
    {
        private readonly IAutoBidService _autoBidService;
        private readonly IAutoBidRepository _autoBidRepository;
        private readonly IRecurringJobManager _recurringJobManager;

        public HangfireAutoBidJobScheduler(IAutoBidService autoBidService, IAutoBidRepository autoBidRepository, IRecurringJobManager recurringJobManager)
        {
            _autoBidService = autoBidService;
            _autoBidRepository = autoBidRepository;
            _recurringJobManager = recurringJobManager;
        }

        public void ScheduleAutoBidJob()
        {
            
            _recurringJobManager.AddOrUpdate(
                "AutoBidJob",
                () => RunAutoBidForAllActiveAssets(),
                "*/5 * * * * *");
        }

        public async Task RunAutoBidForAllActiveAssets()
        {
            Console.WriteLine($"[AutoBid] Job running at {DateTime.UtcNow}");
            var activeAuctionAssets = await _autoBidRepository.GetActiveAuctionAssetPairsAsync();
            foreach (var (auctionId, assetId) in activeAuctionAssets)
            {
                await _autoBidService.RunAutoBidRoundRobin(auctionId, assetId);
            }
        }


    }
}
