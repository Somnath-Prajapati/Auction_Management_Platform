using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.GetFeaturedAssets;
using MediatR;

namespace AuctionManagementSystem.Application.Features.GetFeaturedAssets.Query
{
    public class GetFeaturedAssetsQuery : IRequest<List<FeaturedAssetDto>> { }

}
