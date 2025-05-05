using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Dtos.UserDtos;
using AuctionManagementSystem.Domain.Entities.Asset;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AuctionManagementSystem.Application.Features.Assets.AssetGallery.Query.GetAssetGallery
{
    public class GetAllAssetGalleriesHandler : IRequestHandler<GetAllAssetGalleriesQuery, IEnumerable<AssetsGalleryDto>>
    {
        private readonly IAssetGalleryRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetAllAssetGalleriesHandler(IAssetGalleryRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<AssetsGalleryDto>> Handle(GetAllAssetGalleriesQuery request, CancellationToken cancellationToken)
        {
            var assets = await _repository.GetAllAsync(); // Get all asset galleries from the DB
            var baseUrl = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

            // Map each asset to the DTO and set the ImageUrl based on the FilePath
            var assetDtos = assets.Select(asset =>
            {
                var dto = _mapper.Map<AssetsGalleryDto>(asset);

                // Check if FilePath exists and set ImageUrl
                if (!string.IsNullOrEmpty(asset.FilePath))
                {
                    // Ensure only the relative path after "AssetGallery/" is used
                    var relativePath = asset.FilePath.Replace("AssetGallery/", "");
                    dto.ImageUrl = $"{baseUrl}/AssetGallery/{relativePath}"; // Correct URL
                }

                return dto;
            }).ToList();

            return assetDtos;
        }

    }
}