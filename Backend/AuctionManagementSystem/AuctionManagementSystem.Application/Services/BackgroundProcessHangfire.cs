using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Bids;
using Hangfire;
using Hangfire.Annotations;
using Hangfire.Server;

namespace AuctionManagementSystem.Application.Services
{
    //public class BackgroundProcessHangfire : IBackgroundProcess
    //{
        //private readonly IAutoBidService _autoBidService;
        //private readonly IAutoBidRepository _autoBidRepository;

        //public BackgroundProcessHangfire(IAutoBidService autoBidService, IAutoBidRepository autoBidRepository, IRecurringJobManager recurringJobManager)
        //{
        //    _autoBidService = autoBidService;
        //    _autoBidRepository = autoBidRepository;
        //}

        //public void Execute([NotNull] BackgroundProcessContext context)
        //{
        //    RunAutoBidForAllActiveAssets();
        //    // Wait for 1 hour before next execution
        //    context.Wait(TimeSpan.FromMinutes(1));
        //}

        //public async Task RunAutoBidForAllActiveAssets()
        //{
        //    try
        //    {
        //        Console.WriteLine($"[AutoBid] Job running at {DateTime.UtcNow}");
        //        //var activeAuctionAssets = await _autoBidRepository.GetActiveAuctionAssetPairsAsync();
        //        //foreach (var (auctionId, assetId) in activeAuctionAssets)
        //        //{
        //        //    await _autoBidService.RunAutoBidRoundRobin(auctionId, assetId);
        //        //}
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Error : " + e.ToString());
        //    }

        //}

    //}
}
