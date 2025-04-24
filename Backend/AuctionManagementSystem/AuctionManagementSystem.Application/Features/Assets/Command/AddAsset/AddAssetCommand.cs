using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.Command.AddAsset
{
    public record AddAssetCommand (CreateAssetsDto AssetsDto) : IRequest<CreateAssetsDto>;
}
