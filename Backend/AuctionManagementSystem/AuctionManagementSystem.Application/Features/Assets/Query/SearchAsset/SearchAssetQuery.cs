using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.Query.SearchAsset
{
    public record SearchAssetQuery (string name)  : IRequest<IEnumerable<GetAssetsDto>>;
  
}
