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
    public class AddToWishlistCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public int AssetId { get; set; }
    }


    public class RemoveFromWishlistCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public int AssetId { get; set; }
    }

    public class GetWishlistByUserIdQuery : IRequest<List<DirectSaleAssetDto>>
    {
        public int UserId { get; set; }
    }
}
