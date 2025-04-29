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

namespace AuctionManagementSystem.Application.Features.Assets.Query.GetAssets
{
    public class GetAssetsQueryHandler : IRequestHandler<GetAssetsQuery, IEnumerable<GetAssetsFormDto>>
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetAssetsQueryHandler(IAssetsRepository assetsRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _assetsRepository = assetsRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<GetAssetsFormDto>> Handle(GetAssetsQuery request, CancellationToken cancellationToken)
        {
            var assets = await _assetsRepository.GetAllAsync();
            var baseUrl = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

            // Map assets to GetAssetsFormDto and generate the ImageUrl for each gallery
            var assetDtos = assets.Select(asset =>
            {
                var dto = _mapper.Map<GetAssetsFormDto>(asset);

                foreach (var gallery in dto.Galleries)
                {
                    if (!string.IsNullOrEmpty(gallery.FilePath))
                    {
                        var relativePath = gallery.FilePath.Replace("AssetGallery/", "");
                        gallery.FileUrl = $"{baseUrl}/AssetGallery/{relativePath}";
                    }
                }

                return dto;
            }).ToList();

            return assetDtos;
        }   



        
    }
}
