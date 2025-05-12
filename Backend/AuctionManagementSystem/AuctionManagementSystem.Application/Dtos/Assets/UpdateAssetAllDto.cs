using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class UpdateAssetAllDto
    {
        public int AssetId { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public decimal Deposit { get; set; }
        public int SellerId { get; set; }
        public decimal Commission { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal ReserveAmount { get; set; }
        public int IncrementalTime { get; set; }
        public decimal MinIncrement { get; set; }
        public bool MakeOffer { get; set; }
        public bool Featured { get; set; }
        public int? AwardingId { get; set; }
        public int StatusId { get; set; }
        public int? Vatid { get; set; }
        public decimal Vatpercent { get; set; }
        public string CourtCaseNumber { get; set; }
        public int? RegistrationDeadline { get; set; }
        public string Description { get; set; }
        public decimal? MapLatitude { get; set; }
        public decimal? MapLongitude { get; set; }

        public bool? RequestForViewing { get; set; } 
        public bool? RequestForInquiry { get; set; } 

        public decimal? AdminFees { get; set; }
        public decimal? AuctionFees { get; set; }
        public decimal? BuyerCommission { get; set; }
        public int? WinnerId { get; set; }
        public string AssetNumber { get; set; }
        public decimal? AwardedPrice { get; set; }
        public string SalesNotes { get; set; }

        public List<IFormFile>? NewGalleryImages { get; set; }
        public List<IFormFile>? NewDocuments { get; set; }

        //public List<UpdateAssetDetailDto> Attributes { get; set; }

        [JsonPropertyName("detailsJson")]
        public string DetailsJson { get; set; } = "[]";
    }

    public class UpdateAssetDetailDto
    {
        //public int DetailId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }  
    }

}
