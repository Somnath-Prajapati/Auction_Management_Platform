using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Dtos.Listings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Listings.Cart
{
    public class AddToCartCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public int AssetId { get; set; }
        public int Quantity { get; set; }
    }

    public class RemoveFromCartCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public int AssetId { get; set; }
    }



    public class GetCartByUserIdQuery : IRequest<List<DirectSaleAssetDto>>
    {
        public int UserId { get; set; }
    }

    public class DecreaseCartItemQuantityCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public int AssetId { get; set; }
    }


}
