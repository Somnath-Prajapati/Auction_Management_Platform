using AuctionManagementSystem.Application.Dtos.Assets;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetCategory.Command.UpdateAssetCategory
{
    public class UpdateAssetCategoryCommand : IRequest<bool>
    {
        public int CategoryId { get; set; }
        public UpdateAssetCategoryDto UpdatedCategory { get; set; }

        public UpdateAssetCategoryCommand(int categoryId, UpdateAssetCategoryDto updatedCategory)
        {
            CategoryId = categoryId;
            UpdatedCategory = updatedCategory;
        }
    }
}
