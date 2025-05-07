using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetDetails.Query.GetDetailsById
{
    public record GetAssetDetailByIdQuery(int Id) : IRequest<GetAssetDetailsDto?>;

}
