using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Command.DeleteAsset
{
    public record DeleteAssetCommand(int id) : IRequest;

}
