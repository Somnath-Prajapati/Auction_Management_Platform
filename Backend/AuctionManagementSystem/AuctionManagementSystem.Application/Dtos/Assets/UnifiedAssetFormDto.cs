using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class UnifiedAssetFormDto
    {
        public string Title { get; set; }
        public int? CategoryId { get; set; }
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
        public int? StatusId { get; set; }
        public int? Vatid { get; set; }
        public decimal? Vatpercent { get; set; }
        public string CourtCaseNumber { get; set; }
        public int? RegistrationDeadline { get; set; }
        public string Description { get; set; }
        public decimal? MapLatitude { get; set; }
        public decimal? MapLongitude { get; set; }
        public decimal? AdminFees { get; set; }
        public decimal? AuctionFees { get; set; }
        public decimal? BuyerCommission { get; set; }
        public string SalesNotes { get; set; }
        public string AssetNumber { get; set; }
        public string Details { get; set; }

        //// Collections for files and details
        //public List<AssetGalleryItemDto> Galleries { get; set; } = new List<AssetGalleryItemDto>();
        //public List<AssetDocumentItemDto> Documents { get; set; } = new List<AssetDocumentItemDto>();
        //public List<AssetDetailItemDto> AssetDetails { get; set; } = new List<AssetDetailItemDto>();

        // For galleries
        public List<IFormFile> GalleryFiles { get; set; } = new List<IFormFile>();
        public List<string> GalleryMediaTypes { get; set; } = new List<string>();
        public List<int?> GallerySortOrders { get; set; } = new List<int?>();

        // For documents
        public List<IFormFile> DocumentFiles { get; set; } = new List<IFormFile>();
        public List<string> DocumentTypes { get; set; } = new List<string>();

        //    // For details
        //    public List<string> DetailTypes { get; set; } = new List<string>();
        //    public List<string> DetailValues { get; set; } = new List<string>();
    }

    //public class AssetGalleryItemDto
    //{
    //    public IFormFile File { get; set; }
    //    public string MediaType { get; set; }
    //    public int? SortOrder { get; set; }
    //}

    //public class AssetDocumentItemDto
    //{
    //    public IFormFile File { get; set; }
    //    public string DocumentType { get; set; }
    //}

    //public class AssetDetailItemDto
    //{
    //    public string Type { get; set; }
    //    public string Value { get; set; }
    //}
}
