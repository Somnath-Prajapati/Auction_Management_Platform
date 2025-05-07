using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetCategory.Query.GetAllAssetCategories
{
    public class GetAllAssetCategoriesHandler : IRequestHandler<GetAllAssetCategoriesQuery, List<AssetCategoryDto>>
    {
        private readonly IAssetCategoriesRepository _repository;
        private readonly IMapper _mapper;

        public GetAllAssetCategoriesHandler(IAssetCategoriesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<AssetCategoryDto>> Handle(GetAllAssetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAllAsync();
            return _mapper.Map<List<AssetCategoryDto>>(categories);
        }
    }

}

