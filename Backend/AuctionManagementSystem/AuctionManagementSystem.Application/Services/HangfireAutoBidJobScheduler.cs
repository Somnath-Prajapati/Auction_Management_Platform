using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Bids;
using Hangfire;
using Hangfire.Common;

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
            _recurringJobManager.RemoveIfExists("AutoBidJob");


            //_recurringJobManager.AddOrUpdate(
            //    "AutoBidJob",
            //    Job.FromExpression<IAutoBidJobScheduler>(x => x.RunAutoBidForAllActiveAssets()),
            //    "*/5 * * * * *");

            //_recurringJobManager.AddOrUpdate(
            //        "AutoBidJob",
            //        Job.FromExpression<HangfireAutoBidJobScheduler>(x => x.RunAutoBidForAllActiveAssets()),
            //        "*/5 * * * * *");
            // --------------------------/////////////////////////////////////////////

            _recurringJobManager.AddOrUpdate(
                         "AutoBidJob",
                         Job.FromExpression<HangfireAutoBidJobScheduler>(x => x.RunAutoBidForAllActiveAssets()),
                            "*/3 * * * *");



        }

        public void demo()
        {   
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