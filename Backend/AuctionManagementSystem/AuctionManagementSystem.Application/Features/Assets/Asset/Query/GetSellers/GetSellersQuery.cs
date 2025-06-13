using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Domain.Entities.User;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Query.GetSellers
{
    public record GetSellersQuery : IRequest< IEnumerable<SellerDto>>;
   
}
