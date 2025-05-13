using MediatR;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Application.Features.AssetCategories.Commands.DeleteAssetCategory;
using System.Threading;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Features.Assets.AssetCategory.Command.DeleteAssetCategory;

namespace AuctionManagementSystem.Application.Features.AssetCategories.Commands.DeleteAssetCategory
{
    public class DeleteAssetCategoryHandler : IRequestHandler<DeleteAssetCategoryCommand, bool>
    {
        private readonly IAssetCategoriesRepository _repository;

        public DeleteAssetCategoryHandler(IAssetCategoriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteAssetCategoryCommand request, CancellationToken cancellationToken)
        {
            // Fetch the asset category by CategoryId
            var category = await _repository.GetByIdAsync(request.CategoryId);
            if (category != null && !category.IsDeleted)
            {
                // Perform soft delete by setting IsDeleted to true
                category.IsDeleted = true;
                _repository.Update(category);  // Update the asset category
                await _repository.SaveAsync();  // Save changes
                return true;
            }
            return false;  // Return false if category not found or already deleted
        }
    }
}
