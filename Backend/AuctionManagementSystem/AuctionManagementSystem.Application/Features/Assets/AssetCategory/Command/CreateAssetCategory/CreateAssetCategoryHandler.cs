using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.User; // For IFileService
using AuctionManagementSystem.Application.Exceptions;
using AuctionManagementSystem.Domain.Entities.Asset;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetCategory.Command.CreateAssetCategory
{
    public class CreateAssetCategoryHandler : IRequestHandler<CreateAssetCategoryCommand, int>
    {

        private readonly IAssetCategoriesRepository _repository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public CreateAssetCategoryHandler(IAssetCategoriesRepository repository, IFileService fileService, IMapper mapper)
        {
            _repository = repository;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateAssetCategoryCommand request, CancellationToken cancellationToken)
        {
            // Check for existing category name (case-insensitive)
            var existingCategory = await _repository.GetByNameAsync(request.AssetCategory.CategoryName.Trim());
            if (existingCategory != null)
            {
                throw new BadRequestException("An asset category with the same name already exists.");
            }

            var entity = _mapper.Map<TblAssetCategory>(request.AssetCategory);

            // Save icon file if provided
            if (request.AssetCategory.Icon != null)
            {
                entity.Icon = await _fileService.SaveFileAsync(request.AssetCategory.IconFile, "CategoryIcons");
            }

            if (request.AssetCategory.Document != null)
            {
                entity.DocumentPath = await _fileService.SaveFileAsync(request.AssetCategory.Document, "CategoryDocuments");
            }


            var created = await _repository.AddAsync(entity);
            return created.CategoryId;
        }
    }
}
