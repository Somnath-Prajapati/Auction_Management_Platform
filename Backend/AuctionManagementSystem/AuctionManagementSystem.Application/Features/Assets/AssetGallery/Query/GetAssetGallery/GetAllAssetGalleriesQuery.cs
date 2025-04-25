using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.Asset;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetGallery.Query.GetAssetGallery
{
    public record GetAllAssetGalleriesQuery : IRequest<IEnumerable<TblAssetGallery>>;
}
