using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Query.GetAssetById
{
    public class GetAssetByIdQueryHandler : IRequestHandler<GetAssetByIdQuery, GetAssetsFormDto>
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetAssetByIdQueryHandler(IMapper mapper, IAssetsRepository assetsRepository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _assetsRepository = assetsRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetAssetsFormDto> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
        {
            var asset = await _assetsRepository.GetByIdAsync(request.id);
            var baseUrl = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

            if (asset == null)
                throw new Exception("Asset not found");  // optional: handle null better

            var dto = _mapper.Map<GetAssetsFormDto>(asset);

            if (dto.Galleries != null)
            {
                foreach (var gallery in dto.Galleries)
                {
                    if (!string.IsNullOrEmpty(gallery.FilePath))
                    {
                        var relativePath = gallery.FilePath.Replace("AssetGallery/", "");
                        gallery.FileUrl = $"{baseUrl}/AssetGallery/{relativePath}";
                    }
                }
            }

            return dto;
        }

    }
}
