using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Domain.Entities.Auction;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetAuction.Command.AddAssetAuction
{
    internal class AssignAssetToAuctionCommandHandler : IRequestHandler<AssignAssetToAuctionCommand>
    {
        private readonly IAuctionAssetRepository _repo;

        public AssignAssetToAuctionCommandHandler(IAuctionAssetRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(AssignAssetToAuctionCommand request, CancellationToken cancellationToken)
        {
            var existingAuctionIds = await _repo.GetAssignedAuctionIdsAsync(request.AssetId);

            var newAssignments = request.AuctionIds
                .Where(id => !existingAuctionIds.Contains(id))
                .ToList();

            foreach (var auctionId in newAssignments)
            {
                var auctionAsset = new TblAuctionAsset
                {
                    AssetId = request.AssetId,
                    AuctionId = auctionId
                };

                await _repo.AddAsync(auctionAsset);
            }

            //return Unit.Value;

            Console.WriteLine(Unit.Value);
        }

    }
}