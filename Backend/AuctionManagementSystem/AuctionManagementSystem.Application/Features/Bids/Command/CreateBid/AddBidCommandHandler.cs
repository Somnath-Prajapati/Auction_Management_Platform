using System.Linq.Expressions;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Application.Contracts.RealTime;
using AuctionManagementSystem.Application.Exceptions;
using AuctionManagementSystem.Domain.Entities.Bids;
using AutoMapper;
using MediatR;
 

namespace AuctionManagementSystem.Application.Features.Bids.Command.CreateBid
{
    public class AddBidCommandHandler : IRequestHandler<AddBidCommand, int>
    {
        private readonly IUnitOfWorkAuth _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBidRepository _bidRepository;
        private readonly IAuctionAssetRepository _auctionAssetRepository;
        private readonly IAssetsRepository _assetsRepository;
        private readonly IAuctionRepository _auctionRepository;
        private readonly IBidNotificationService _notificationService;


        public AddBidCommandHandler(IBidRepository bidRepository, IMapper mapper, IAuctionAssetRepository auctionAssetRepository, IUnitOfWorkAuth unitOfWork, IAssetsRepository assetsRepository, IAuctionRepository auctionRepository, IBidNotificationService notificationService)
        {
            _bidRepository = bidRepository;
            _mapper = mapper;
            _auctionAssetRepository = auctionAssetRepository;
            _unitOfWork = unitOfWork;
            _assetsRepository = assetsRepository;
            _auctionRepository = auctionRepository;
            _notificationService = notificationService;
        }

        public async Task<int> Handle(AddBidCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {


                bool isValid = await _auctionAssetRepository.AssetExistsInAuctionAsync(
                request.AuctionId, request.AssetId);

                if (!isValid)
                    throw new BadRequestException("The asset does not belong to the specified auction.");

                var auction = await _auctionRepository.GetByIdAsync(request.AuctionId);
                if (auction == null || auction.EndDateTime < DateTime.UtcNow || auction.IsDeleted)
                {
                    throw new NotFoundException ("Cannot place a bid. The auction has expired or is inactive.");
                }

                var asset = await _assetsRepository.GetByIdAsync(request.AssetId);
                if (asset == null)
                    throw new NotFoundException("Asset not found.");



                var highestBid = await _bidRepository.GetHighestBidAmountAsync(request.AssetId);
                if (!highestBid.HasValue)
                {
                    if (request.BidAmount < asset.StartingPrice)
                        throw new BadRequestException($"First bid must be at least the starting price: {asset.StartingPrice}");
                }
                else
                {
                    var requiredMinBid = highestBid.Value + asset.MinIncrement;
                    if (request.BidAmount < requiredMinBid)
                        throw new BadRequestException($"Bid must be at least {requiredMinBid} (Min Increment: {asset.MinIncrement})");
                }

                await _bidRepository.UnsetPreviousWinningBidAsync(request.AssetId);

                
                var bid = _mapper.Map<tblBid>(request);
                bid.IsWinningBid = true;

                var bidId = await _bidRepository.AddBidAsync(bid);
                await _unitOfWork.CommitAsync();

                var updatedBidCount = await _bidRepository.CountBidsByAssetIdAsync(request.AssetId);
                await _notificationService.NotifyNewBidAsync(
                   request.AuctionId,
                   request.AssetId,
                   new
                   {
                       bidCount = updatedBidCount,
                       auctionId = request.AuctionId,
                       assetId = request.AssetId,
                       UserId = request.UserId,
                       BidAmount = request.BidAmount,
                       BidTime = DateTime.UtcNow
                   });

                return bidId;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

    }

}
