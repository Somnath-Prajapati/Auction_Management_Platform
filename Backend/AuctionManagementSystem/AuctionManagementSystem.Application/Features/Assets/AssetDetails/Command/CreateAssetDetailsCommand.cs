using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetDetails.Command
{
    public class CreateAssetDetailsCommand : IRequest<List<TblAssetDetail>>
    {
        public List<AssetDetailDto> AssetDetails { get; set; }
    }

}
