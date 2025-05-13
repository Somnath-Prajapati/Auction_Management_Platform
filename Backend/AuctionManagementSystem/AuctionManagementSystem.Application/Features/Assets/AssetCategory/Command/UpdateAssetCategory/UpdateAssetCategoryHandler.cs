using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.User; // For IFileService
using AuctionManagementSystem.Application.Features.Assets.AssetCategory.Command.UpdateAssetCategory;
using AuctionManagementSystem.Domain.Entities.Asset;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.AssetCategories.Commands.UpdateAssetCategory
{
    public class UpdateAssetCategoryHandler : IRequestHandler<UpdateAssetCategoryCommand, bool>
    {
        private readonly IAssetCategoriesRepository _repository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public UpdateAssetCategoryHandler(IAssetCategoriesRepository repository, IMapper mapper, IFileService fileService)
        {
            _repository = repository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<bool> Handle(UpdateAssetCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = await _repository.GetByIdAsync(request.CategoryId);

            if (existingCategory == null || existingCategory.IsDeleted)
                return false;

            var existingIcon = existingCategory.Icon;

            _mapper.Map(request.UpdatedCategory, existingCategory);

            // Handle icon image update
            if (request.UpdatedCategory.IconFile != null)
            {
                if (!string.IsNullOrEmpty(existingIcon))
                    await _fileService.DeleteFileAsync(existingIcon);

                existingCategory.Icon = await _fileService.SaveFileAsync(request.UpdatedCategory.IconFile, "CategoryIcons");
            }
            else
            {
                // If no new file uploaded, retain old icon path
                existingCategory.Icon = existingIcon;
            }

            // Handle document update
            if (request.UpdatedCategory.Document != null)
            {
                existingCategory.DocumentPath = await _fileService.SaveFileAsync(request.UpdatedCategory.Document, "CategoryDocuments");
            }

            existingCategory.UpdatedDate = DateTime.UtcNow;

            _repository.Update(existingCategory);
            await _repository.SaveAsync();

            return true;
        }

    }
}
