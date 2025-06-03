using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Dtos.Bids;
using AuctionManagementSystem.Domain.Entities.Asset;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetWinner.Query.GetByUserIdAsync
{
    public record GetByUserIdAsyncQuery(int userId) : IRequest<List<AssetWinnerDto>>;

}
