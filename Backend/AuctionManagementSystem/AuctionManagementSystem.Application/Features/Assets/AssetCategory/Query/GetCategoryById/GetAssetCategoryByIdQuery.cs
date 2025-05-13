using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetCategory.Query.GetCategoryById
{
    public record GetAssetCategoryByIdQuery(int id) : IRequest<AssetCategoryDto>;
}
