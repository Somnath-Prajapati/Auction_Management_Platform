using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Command.AddWinner
{
    public class AddOrReplaceWinnerCommandHandler : IRequestHandler<AddOrReplaceWinnerCommand, int>
    {
         private readonly IAssetsRepository _assetsRepository;

        public AddOrReplaceWinnerCommandHandler(IAssetsRepository assetsRepository)
        {
            _assetsRepository = assetsRepository;
        }

        public Task<int> Handle(AddOrReplaceWinnerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
