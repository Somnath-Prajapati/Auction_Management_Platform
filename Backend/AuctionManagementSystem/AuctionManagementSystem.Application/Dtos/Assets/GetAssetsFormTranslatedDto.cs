using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class GetAssetsFormTranslatedDto
    {
        public int AssetId { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; }
        public string? TitleTranslated { get; set; }

        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public decimal? Deposit { get; set; }
        public int SellerId { get; set; }
        public decimal? Commission { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal? ReserveAmount { get; set; }
        public int? IncrementalTime { get; set; }
        public decimal? MinIncrement { get; set; }
        public bool? MakeOffer { get; set; }
        public bool? Featured { get; set; }
        public int? AwardingId { get; set; }
        public string? AwardingMethod { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
        public int? Vatid { get; set; }
        public string? VatType { get; set; }
        public decimal? Vatpercent { get; set; }
        public string? CourtCaseNumber { get; set; }
        public int? RegistrationDeadline { get; set; }

        public string? Description { get; set; }
        public string? DescriptionTranslated { get; set; }

        public string? SalesNotes { get; set; }
        public string? SalesNotesTranslated { get; set; }

        public decimal? MapLatitude { get; set; }
        public decimal? MapLongitude { get; set; }
        public decimal? AdminFees { get; set; }
        public decimal? AuctionFees { get; set; }
        public decimal? BuyerCommission { get; set; }
        public bool? RequestForViewing { get; set; }
        public bool? RequestForInquiry { get; set; }

        public int? WinnerId { get; set; }
        public string? WinnerName { get; set; }
        public decimal? AwardedPrice { get; set; }

        public string? AssetNumber { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsAvailableForDirectSale { get; set; }

        public List<int> AuctionIds { get; set; }
        public int? AuctionStatusId { get; set; }

        public bool isDeleted { get; set; }

        public List<AssetGalleryDtos> Galleries { get; set; } = new();
        public List<AssetDocumentFormDto> Documents { get; set; } = new();
        public List<AssetDetailDtoo> Attributes { get; set; } = new();
    }

}
