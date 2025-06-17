using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Command.AddNewWinner
{
    public class ReplaceAssetWinnerCommandHandler : IRequestHandler<ReplaceAssetWinnerCommand, int>
    {
        private readonly IAssetsRepository _assetRepository;

            
        public ReplaceAssetWinnerCommandHandler(IAssetsRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task<int> Handle(ReplaceAssetWinnerCommand request, CancellationToken cancellationToken)
        {
            var dto = request.dto;
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "ReplaceAssetWinnerDto cannot be null");
            }

            return await _assetRepository.ReplaceAssetWinnerAsync(
            dto.AssetId,
            dto.UserId,
            dto.AwardedPrice,
            dto.Reason,
            dto.Note,
            dto.Approved
            );
        }
    }
}
