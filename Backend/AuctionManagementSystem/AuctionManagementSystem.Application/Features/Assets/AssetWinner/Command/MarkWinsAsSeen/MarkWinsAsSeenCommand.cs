using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetWinner.Command.MarkWinsAsSeen
{
    public record MarkWinsAsSeenCommand(int userId) : IRequest<int>;
}
