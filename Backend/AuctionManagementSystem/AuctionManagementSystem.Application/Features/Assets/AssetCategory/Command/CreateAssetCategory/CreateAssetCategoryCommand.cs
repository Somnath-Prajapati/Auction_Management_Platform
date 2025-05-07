using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.DTOs.Assets.AssetCategory;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetCategory.Command.CreateAssetCategory
{
    public class CreateAssetCategoryCommand : IRequest<int>
    {
        public CreateAssetCategoryDto AssetCategory { get; set; }

        public CreateAssetCategoryCommand(CreateAssetCategoryDto assetCategory)
        {
            AssetCategory = assetCategory;
        }
    }
}
