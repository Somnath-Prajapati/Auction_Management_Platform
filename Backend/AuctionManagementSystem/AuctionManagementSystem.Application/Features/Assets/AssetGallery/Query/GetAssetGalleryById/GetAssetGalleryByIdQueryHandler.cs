using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AuctionManagementSystem.Application.Features.Assets.AssetGallery.Query.GetAssetGalleryById
{
    public class GetAssetGalleryByIdHandler : IRequestHandler<GetAssetGalleryByIdQuery, AssetsGalleryDto?>
    {
        private readonly IAssetGalleryRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public GetAssetGalleryByIdHandler(IAssetGalleryRepository repository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<AssetsGalleryDto?> Handle(GetAssetGalleryByIdQuery request, CancellationToken cancellationToken)
        {
            var asset = await _repository.GetByIdAsync(request.Id);

            // Check if asset is null and return null if not found
            if (asset == null) return null;

            var baseUrl = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

            // Map asset to DTO
            var dto = _mapper.Map<AssetsGalleryDto>(asset);

            // Check if FilePath exists and set ImageUrl correctly
            if (!string.IsNullOrEmpty(asset.FilePath))
            {
                // Ensure only the relative path after "AssetGallery/" is used
                var relativePath = asset.FilePath.Replace("AssetGallery/", "");
                dto.ImageUrl = $"{baseUrl}/AssetGallery/{relativePath}"; // Correct URL format
            }

            return dto;
        }

    }
}
