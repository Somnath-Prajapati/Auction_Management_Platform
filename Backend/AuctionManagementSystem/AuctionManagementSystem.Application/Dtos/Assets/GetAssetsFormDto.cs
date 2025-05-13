using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class GetAssetsFormDto
    {
        public int AssetId { get; set; }
        public string Title { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; } // New Field
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
        public string? AwardingMethod { get; set; } // New Field
        public int? StatusId { get; set; }
        public string? StatusName { get; set; } // New Field
        public int? Vatid { get; set; }
        public string? VatType { get; set; } // New Field
        public decimal? Vatpercent { get; set; }
        public string? CourtCaseNumber { get; set; }
        public int? RegistrationDeadline { get; set; }
        public string? Description { get; set; }
        public decimal? MapLatitude { get; set; }
        public decimal? MapLongitude { get; set; }
        public decimal? AdminFees { get; set; }
        public decimal? AuctionFees { get; set; }
        public decimal? BuyerCommission { get; set; }
        public bool? RequestForViewing { get; set; } 
        public bool? RequestForInquiry { get; set; }
        public int? WinnerId { get; set; }
        public string? WinnerName { get; set; } 
        public decimal? AwardedPrice { get; set; } // From Winner
        public string? SalesNotes { get; set; }
        public string Details { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsAvailableForDirectSale { get; set; } // for asset direct sale



        public List<int> AuctionIds { get; set; }


        public int? AuctionStatusId { get; set; }

        // auction 




        public string AssetNumber { get; set; }
        public List<AssetGalleryDtos> Galleries { get; set; } = new();
        public List<AssetDocumentFormDto> Documents { get; set; } = new();

        public List<AssetDetailDtoo> Attributes { get; set; }
    }

    public class AssetGalleryDtos
    {
        public string? MediaType { get; set; }
        public string? FilePath { get; set; }
        public string FileUrl { get; set; } 
        public int? SortOrder { get; set; }
    }
        

    public class AssetDocumentFormDto
    {
        public int DocumentId { get; set; }
        public string? DocumentType { get; set; }
        public string? FilePath { get; set; }

        public string FileUrl { get; set; }
    }

    public class AssetDetailDtoo
    {
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
    }


}
