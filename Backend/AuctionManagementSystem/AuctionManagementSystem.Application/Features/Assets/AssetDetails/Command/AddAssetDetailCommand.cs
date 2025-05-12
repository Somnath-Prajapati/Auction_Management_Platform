using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.Asset;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetDetails.Command
{
    public class AddAssetDetailCommand : IRequest<int>  
    {
        public TblAssetDetail AssetDetail { get; set; }

        public AddAssetDetailCommand(TblAssetDetail assetDetail)
        {
            AssetDetail = assetDetail;
        }
    }
}
