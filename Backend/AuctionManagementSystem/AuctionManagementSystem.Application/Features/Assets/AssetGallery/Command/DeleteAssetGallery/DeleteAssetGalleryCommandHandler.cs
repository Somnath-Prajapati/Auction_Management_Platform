using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetGallery.Command.DeleteAssetGallery
{
    public class DeleteAssetGalleryHandler : IRequestHandler<DeleteAssetGalleryCommand, bool>
    {
        private readonly IAssetGalleryRepository _repository;

        public DeleteAssetGalleryHandler(IAssetGalleryRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteAssetGalleryCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteAsync(request.Id);
        }
    }
}
