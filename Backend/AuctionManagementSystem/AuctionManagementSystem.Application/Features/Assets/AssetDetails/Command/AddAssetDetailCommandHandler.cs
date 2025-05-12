using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetDetails.Command
{
    public class AddAssetDetailCommandHandler : IRequestHandler<AddAssetDetailCommand, int>
    {
        private readonly IAssetDetailRepository _assetDetailRepository;

        public AddAssetDetailCommandHandler(IAssetDetailRepository assetDetailRepository)
        {
            _assetDetailRepository = assetDetailRepository;
        }

        public async Task<int> Handle(AddAssetDetailCommand request, CancellationToken cancellationToken)
        {
            // Save the asset detail to the database
            return await _assetDetailRepository.AddAsync(request.AssetDetail);
        }
    }
}
