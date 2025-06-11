using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Features.Assets.Asset.Query.GetAssetById;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Query.GetAssetsForView
{
    public class GetAssetsByIdForViewQueryHandler : IRequestHandler<GetAssetByIdQuery, GetAssetsFormDto>
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetAssetsByIdForViewQueryHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor, IAssetsRepository assetsRepository)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _assetsRepository = assetsRepository;
        }

        public async Task<GetAssetsFormDto> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
        {
            var asset = await _assetsRepository.GetByIdViewAsync(request.id,request.lang);
            var baseUrl = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

            if (asset == null)
                throw new Exception("Asset not found");

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

            foreach (var doc in dto.Documents)
            {
                if (!string.IsNullOrEmpty(doc.FilePath))
                {
                C:
                    var relativePath = doc.FilePath.Replace("AssetDocuments/", "");
                    doc.FileUrl = $"{baseUrl}/AssetDocuments/{relativePath}";
                }
            }

            return dto;
        }
    }
}
