using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AuctionManagementSystem.Domain.Entities.Auction;
using AuctionManagementSystem.Domain.Entities.Bids;
using AuctionManagementSystem.Domain.Entities.Request;
using AuctionManagementSystem.Domain.Entities.Transaction;
using AuctionManagementSystem.Domain.Entities.User;
using AuctionManagementSystem.Persistence.TempEntities;

namespace AuctionManagementSystem.Domain.Entities.Asset;

public partial class TblAsset
{
    public int AssetId { get; set; }

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

    public int? WinnerId { get; set; }

    public string SalesNotes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; } = true;

    public string AssetNumber { get; set; }

    //[Column("Details")]
    //public string Details { get; set; }

    public bool? RequestForViewing { get; set; } = true;

    public bool? RequestForInquiry { get; set; } = true;


    public virtual TblWinnerAwardingOption Awarding { get; set; }

    public virtual TblAssetCategory Category { get; set; }

    public virtual TblSeller Seller { get; set; }

    public virtual TblAssetStatus Status { get; set; }

    public virtual ICollection<TblAssetDetail> TblAssetDetails { get; set; } = new List<TblAssetDetail>();

    public virtual ICollection<TblAssetDocument> TblAssetDocuments { get; set; } = new List<TblAssetDocument>();

    public virtual ICollection<TblAssetGallery> TblAssetGalleries { get; set; } = new List<TblAssetGallery>();

    public virtual ICollection<TblAssetWinner> TblAssetWinners { get; set; } = new List<TblAssetWinner>();

    public virtual ICollection<TblAuctionAsset> TblAuctionAssets { get; set; } = new List<TblAuctionAsset>();

    public virtual ICollection<TblRequest> TblRequests { get; set; } = new List<TblRequest>();

    public virtual ICollection<TblTransactionAsset> TblTransactionAssets { get; set; } = new List<TblTransactionAsset>();
    public virtual ICollection<TblOrderAsset> TblOrderAssets { get; set; } = new List<TblOrderAsset>();

    public virtual TblVatoption Vat { get; set; }
    public virtual ICollection<tblBid> TblBids { get; set; } = new List<tblBid>();
    public virtual TblAssetWinner Winner { get; set; }
    public virtual ICollection<TblWishlistItem> TblWishlistItems { get; set; } = new List<TblWishlistItem>();
    public virtual ICollection<TblCartItem> TblCartItems { get; set; } = new List<TblCartItem>();
    public bool IsAvailableForDirectSale { get; set; }
}