using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetCategory.Command.CreateAssetCategory
{
    public class CreateAssetCategoryHandler : IRequestHandler<CreateAssetCategoryCommand, int>
    {
        private readonly IAssetCategoriesRepository _repository;
        private readonly IMapper _mapper;

        public CreateAssetCategoryHandler(IAssetCategoriesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateAssetCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TblAssetCategory>(request.AssetCategory);
            var created = await _repository.AddAsync(entity);
            return created.CategoryId;
        }
    }
}
