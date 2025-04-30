using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetDetails.Command
{
    public class CreateAssetDetailsCommandHandler : IRequestHandler<CreateAssetDetailsCommand, List<TblAssetDetail>>
    {
        private readonly IAssetDetailRepository _assetDetailRepository;

        public CreateAssetDetailsCommandHandler(IAssetDetailRepository assetDetailRepository)
        {
            _assetDetailRepository = assetDetailRepository;
        }

        public async Task<List<TblAssetDetail>> Handle(CreateAssetDetailsCommand request, CancellationToken cancellationToken)
        {
            var assetDetails = request.AssetDetails.Select(x => new TblAssetDetail
            {
                AssetId = x.AssetId,
                AttributeName = x.AttributeName,
                AttributeValue = x.AttributeValue
            }).ToList();

            // Call repository to insert records
            await _assetDetailRepository.AddAssetDetailsAsync(assetDetails);

            return assetDetails;
        }
    }
}
