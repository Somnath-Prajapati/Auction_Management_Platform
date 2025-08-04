using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.Bids;
using Hangfire;
using Hangfire.Common;

namespace AuctionManagementSystem.Application.Services
{
    public class HangfireAutoBidJobScheduler 
    {
        private readonly IAutoBidService _autoBidService;
        private readonly IAutoBidRepository _autoBidRepository;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IAssetsRepository _assetRepository;

        public HangfireAutoBidJobScheduler(IAutoBidService autoBidService,
            IAutoBidRepository autoBidRepository, IRecurringJobManager recurringJobManager, IAssetsRepository assetRepository)
        {
            _autoBidService = autoBidService;
            _autoBidRepository = autoBidRepository;
            _recurringJobManager = recurringJobManager;
            _assetRepository = assetRepository;
        }

        public void ScheduleAutoBidJob()
        {
            _recurringJobManager.RemoveIfExists("AutoBidJob");

            _recurringJobManager.AddOrUpdate<HangfireAutoBidJobScheduler>(
                     "AutoBidJob",
                       x => x.RunAutoBidForAllActiveAssets(),
                       "*/50 * * * * *"
            );

        }

        public void RunRemainingDays()
        {
            _recurringJobManager.RemoveIfExists("UpdateRemaniningDays");

            _recurringJobManager.AddOrUpdate<HangfireAutoBidJobScheduler>(
                     "UpdateRemaniningDays",
                     x => x.RunRemainingDay(),
                     Cron.Minutely
            );

        }





        public async Task RunRemainingDay()
        {
            await _assetRepository.UpdateRemaningDays();
        }
        public void demo()
        {   
            var count = 0;
            var ans = count++;
            Console.WriteLine("job schedule : " + DateTime.Now.ToString());
        }

        public async Task RunAutoBidForAllActiveAssets()
            {
            try
            {
                Console.WriteLine($"[AutoBid] Job running at {DateTime.UtcNow}");
                var activeAuctionAssets = await _autoBidRepository.GetActiveAuctionAssetPairsAsync();
                foreach (var (auctionId, assetId) in activeAuctionAssets)
                {
                    await _autoBidService.RunAutoBidRoundRobin(auctionId, assetId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.ToString());
            }

        }
    }
}
            
