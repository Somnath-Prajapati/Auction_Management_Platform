using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class CreateAssetsDto
    {

        public string Title { get; set; }
        public int CategoryId { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal? ReserveAmount { get; set; }
        public decimal? Deposit { get; set; }
        public decimal? Commission { get; set; }
        public int? SellerId { get; set; }
        public int? StatusId { get; set; }
        public int? Vatid { get; set; }
        public decimal? Vatpercent { get; set; }
        public int? AwardingId { get; set; }
        public bool MakeOffer { get; set; }
        public bool Featured { get; set; }
        public string? CourtCaseNumber { get; set; }
        public int RegistrationDeadline { get; set; }
        public string? Description { get; set; }
        public string? MapLatitude { get; set; }
        public string? MapLongitude { get; set; }
        public decimal? AdminFees { get; set; }
        public decimal? AuctionFees { get; set; }
        public decimal? BuyerCommission { get; set; }
        public int? WinnerId { get; set; }
        public decimal? AwardedPrice { get; set; }
        public string? SalesNotes { get; set; }
        public string? AssetNumber { get; set; }
        public int? IncrementalTime { get; set; }
        public int? MinIncrement { get; set; }

        public bool RequestForViewing { get; set; } = true;  
        public bool RequestForInquiry { get; set; } = true;

        public bool IsAvailableForDirectSale { get; set; }
        public int? LanguageId { get; set; }
        public string? TranslatedTitle { get; set; }
        public string? TranslatedDescription { get; set; }
        public string? TranslatedSalesNotes { get; set; }


        public List<IFormFile> GalleryFiles { get; set; } = new List<IFormFile>();
        public List<IFormFile> DocumentFiles { get; set; } = new List<IFormFile>();

        
        [JsonPropertyName("detailsJson")]
        public string DetailsJson { get; set; } = "[]"; 


        // for assetauction 
        public List<int> AuctionIds { get; set; } = new();

    }
    public class AssetGalleryMetaDto
    {
        public string MediaType { get; set; }
        public int SortOrder { get; set; }
        public string? FilePath { get; set; }
    }
    public class AssetDocumentMetaDto
    {
        public string DocumentType { get; set; }
        public string? FilePath { get; set; }
    }

    public class AssetDetailDtos
    {
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
    }
}



