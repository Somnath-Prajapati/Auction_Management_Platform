using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetCategory.Command.DeleteAssetCategory
{
    public class DeleteAssetCategoryCommand : IRequest<bool>
    {
        public int CategoryId { get; set; }
    }
}
