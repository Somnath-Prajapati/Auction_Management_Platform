
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetCategory.Query.GetCategoryById
{
    public class GetAssetCategoryByIdHandler : IRequestHandler<GetAssetCategoryByIdQuery, AssetCategoryDto>
    {
        private readonly IAssetCategoriesRepository _context;

        private readonly IMapper _mapper;
        public GetAssetCategoryByIdHandler(IAssetCategoriesRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<AssetCategoryDto> Handle(GetAssetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.GetByIdAsync(request.id);

            if (entity == null || entity.IsDeleted)
            {
                throw new KeyNotFoundException($"Asset Category with ID {request.id} not found.");
            }

            var dto = _mapper.Map<AssetCategoryDto>(entity);
            return dto;


        }
    }
}
