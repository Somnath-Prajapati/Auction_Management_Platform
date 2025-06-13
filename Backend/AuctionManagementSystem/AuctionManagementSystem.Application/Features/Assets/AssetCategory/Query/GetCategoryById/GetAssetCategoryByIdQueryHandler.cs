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

namespace AuctionManagementSystem.Application.Features.Assets.AssetCategory.Query.GetCategoryById
{
    public class GetAssetCategoryByIdHandler : IRequestHandler<GetAssetCategoryByIdQuery, AssetCategoryDto>
    {
        private readonly IAssetCategoriesRepository _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAssetCategoryByIdHandler(IAssetCategoriesRepository context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AssetCategoryDto> Handle(GetAssetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.GetByIdAsync(request.id);

            if (entity == null || entity.IsDeleted)
            {
                throw new KeyNotFoundException($"Asset Category with ID {request.id} not found.");
            }

            var dto = _mapper.Map<AssetCategoryDto>(entity);

            var baseUrl = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

            dto.Icon = entity.Icon != null ? $"{baseUrl}/{entity.Icon}" : null;
            dto.Document = entity.DocumentPath != null ? $"{baseUrl}/{entity.DocumentPath}" : null;

            dto.PaymentMethodIds = entity.TblAssetCategoryPaymentMethods?
                                        .Select(pm => pm.PaymentMethodId)
                                        .ToList();

            var translation = await _context.GetTranslationByLanguageIdAsync(request.id, 2);

            if (translation != null)
            {
                dto.TranslatedCategoryName = translation.TranslatedCategoryName;
                dto.TranslatedSubcategory = translation.TranslatedSubcategory;
                dto.TranslatedDetails = translation.TranslatedDetails;
                dto.LanguageId = 2;
            }

            return dto;
        }
    }
}
