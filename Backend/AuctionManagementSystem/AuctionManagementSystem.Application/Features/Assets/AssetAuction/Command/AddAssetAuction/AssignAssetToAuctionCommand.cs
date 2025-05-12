using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetAuction.Command.AddAssetAuction
{
    public class AssignAssetToAuctionCommand : IRequest
    {
        public int AssetId { get; set; }
        public List<int> AuctionIds { get; set; }

        public AssignAssetToAuctionCommand(int assetId, List<int> auctionIds)
        {
            AssetId = assetId;
            AuctionIds = auctionIds;
        }
    }
}
