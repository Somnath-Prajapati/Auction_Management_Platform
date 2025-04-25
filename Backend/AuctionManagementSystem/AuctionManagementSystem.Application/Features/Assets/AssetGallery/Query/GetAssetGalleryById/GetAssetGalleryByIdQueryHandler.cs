using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetGallery.Query.GetAssetGalleryById
{
    public class GetAssetGalleryByIdHandler : IRequestHandler<GetAssetGalleryByIdQuery, TblAssetGallery?>
    {
        private readonly IAssetGalleryRepository _repository;

        public GetAssetGalleryByIdHandler(IAssetGalleryRepository repository)
        {
            _repository = repository;
        }

        public async Task<TblAssetGallery?> Handle(GetAssetGalleryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}
