using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Command.DeleteAsset
{
    public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand>
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IMapper _mapper;

        public DeleteAssetCommandHandler(IAssetsRepository assetsRepository, IMapper mapper)
        {
            _assetsRepository = assetsRepository;
            _mapper = mapper;
        }

        public async Task Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
        {
            var Asset = await _assetsRepository.GetIdDeleteAsync(request.id);
            await _assetsRepository.DeleteAsync(Asset);
        }
    }
}
