using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Command
{
    public record AddUnifiedAssetCommand (CreateAssetsDto dto) : IRequest<int>;

}
