//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AuctionManagementSystem.Application.Contracts.Bids;

//namespace AuctionManagementSystem.Application.Services
//{
//    public class AutoBidQuartzJob : IJob
//    {
//        private readonly IAutoBidService _autoBidService;
//        private readonly IAutoBidRepository _autoBidRepository;

//        public AutoBidQuartzJob(IAutoBidService autoBidService, IAutoBidRepository autoBidRepository)
//        {
//            _autoBidService = autoBidService;
//            _autoBidRepository = autoBidRepository;
//        }

//        public async Task Execute(IJobExecutionContext context)
//        {
//            Console.WriteLine($"[Quartz] AutoBid running at {DateTime.UtcNow}");

//            var activeAuctionAssets = await _autoBidRepository.GetActiveAuctionAssetPairsAsync();

//            foreach (var (auctionId, assetId) in activeAuctionAssets)
//            {
//                await _autoBidService.RunAutoBidRoundRobin(auctionId, assetId);
//            }
//        }
//    }
//}