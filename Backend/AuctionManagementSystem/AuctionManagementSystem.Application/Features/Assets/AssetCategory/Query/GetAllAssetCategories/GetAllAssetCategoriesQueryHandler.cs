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

namespace AuctionManagementSystem.Application.Features.Assets.AssetCategory.Query.GetAllAssetCategories
{
    public class GetAllAssetCategoriesHandler : IRequestHandler<GetAllAssetCategoriesQuery, List<AssetCategoryDto>>
    {
        private readonly IAssetCategoriesRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public GetAllAssetCategoriesHandler(IAssetCategoriesRepository repository, IMapper mapper,IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<List<AssetCategoryDto>> Handle(GetAllAssetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var baseUrl = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

            var categories = await _repository.GetAllAsync();

            var categoryDtos = categories.Select(category =>
            {
                var dto = _mapper.Map<AssetCategoryDto>(category);

                // Assuming AssetCategoryDto has properties like Icon or any image properties
                dto.Icon = category.Icon != null ? $"{baseUrl}/{category.Icon}" : null;
                dto.Document = category.DocumentPath != null ? $"{baseUrl}/{category.DocumentPath}" : null;

                return dto;
            }).ToList();

            return categoryDtos;
        }

    }

}

