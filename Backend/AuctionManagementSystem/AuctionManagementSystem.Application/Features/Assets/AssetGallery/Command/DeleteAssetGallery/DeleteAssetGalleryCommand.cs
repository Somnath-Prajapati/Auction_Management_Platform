using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetGallery.Command.DeleteAssetGallery
{
    public record DeleteAssetGalleryCommand(int Id) : IRequest<bool>;
}
