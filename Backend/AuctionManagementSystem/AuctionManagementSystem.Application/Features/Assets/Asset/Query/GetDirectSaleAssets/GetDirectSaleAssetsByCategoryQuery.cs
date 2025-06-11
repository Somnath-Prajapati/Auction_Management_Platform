using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Query.GetDirectSaleAssets
{
     public class GetDirectSaleAssetsByCategoryQuery : IRequest<List<DirectSaleAssetDto>>
    {
        public int CategoryId { get; set; }
        public string lang { get; set; }
    }
 
    public class GetAuctionAssetsByCategoryQuery : IRequest<List<DirectSaleAssetDto>>
    {
        public int CategoryId { get; set; }
        public string lang { get; set; }
    }
}
