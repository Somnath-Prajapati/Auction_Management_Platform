using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class DirectSaleAssetDto
    {
        public int AssetId { get; set; }
        public string Title { get; set; }
        public int? CategoryId { get; set; }
        public decimal? Deposit { get; set; }
        public decimal? MinIncrement { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string SalesNotes { get; set; }
        public decimal Price { get; set; }
        public string ThumbnailUrl { get; set; }
        public string CategoryName { get; set; }

        public bool IsAvailableForDirectSale { get; set; }

        public int? AuctionId { get; set; }
        public string AssetNumber { get; set; }
        //public List<AssetsGalleryDto> Galleries { get; set; } = new List<AssetsGalleryDto>();  // Default to an empty list to prevent null reference issues
        public List<AssetGalleryDtos> Galleries { get; set; } = new();

    }
}
