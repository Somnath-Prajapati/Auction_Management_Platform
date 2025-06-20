using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.GetFeaturedAssets
{
    // DTO
    public class FeaturedAssetDto
    {
        public int AssetId { get; set; }
        public string? Title { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public decimal Deposit { get; set; }
        public int SellerId { get; set; }
        public decimal Commission { get; set; }
        public decimal StartingPrice { get; set; }
        public int IncrementalTime { get; set; }
        public decimal MinIncrement { get; set; }
        public bool MakeOffer { get; set; }
        public bool Featured { get; set; }
        public int AwardingId { get; set; }
        public int StatusId { get; set; }
        public string? StatusName { get; set; }
        public int VATId { get; set; }
        public decimal VATPercent { get; set; }
        public string? CourtCaseNumber { get; set; }
        public DateTime? RegistrationDeadline { get; set; }
        public string? Description { get; set; }
        public string?  MapLatitude { get; set; }
        public string? MapLongitude { get; set; }
        public decimal AdminFees { get; set; }
        public decimal AuctionFees { get; set; }
        public decimal BuyerCommission { get; set; }
        public int? WinnerId { get; set; }
        public string? AssetNumber { get; set; }
        public bool RequestForViewing { get; set; }
        public bool RequestForInquiry { get; set; }
        public string? GalleryFilePaths { get; set; }
        public string? DocumentFilePaths { get; set; }
    }

}
