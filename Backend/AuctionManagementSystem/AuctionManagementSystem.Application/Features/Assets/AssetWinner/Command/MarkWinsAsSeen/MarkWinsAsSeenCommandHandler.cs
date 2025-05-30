using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Exceptions;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetWinner.Command.MarkWinsAsSeen
{
    public class MarkWinsAsSeenCommandHandler : IRequestHandler<MarkWinsAsSeenCommand, int>
    {
        private readonly IAssetWinnerRepository _assetWinnerRepository;
        public MarkWinsAsSeenCommandHandler(IAssetWinnerRepository assetWinnerRepository)
        {
            _assetWinnerRepository = assetWinnerRepository;
        }
        public async Task<int> Handle(MarkWinsAsSeenCommand request, CancellationToken cancellationToken)
        {

            var result = await _assetWinnerRepository.MarskAsSeenAsync(request.userId);
            if(result == null)
            {
                throw new BadRequestException("No Wins Found");
            }
            return result;
        }
    }
}
