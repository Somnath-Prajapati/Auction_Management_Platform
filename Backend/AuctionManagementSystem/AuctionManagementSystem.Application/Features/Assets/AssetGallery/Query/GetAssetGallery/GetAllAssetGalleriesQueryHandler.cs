using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetGallery.Query.GetAssetGallery
{
    public class GetAllAssetGalleriesHandler : IRequestHandler<GetAllAssetGalleriesQuery, IEnumerable<TblAssetGallery>>
    {
        private readonly IAssetGalleryRepository _repository;

        public GetAllAssetGalleriesHandler(IAssetGalleryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TblAssetGallery>> Handle(GetAllAssetGalleriesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}