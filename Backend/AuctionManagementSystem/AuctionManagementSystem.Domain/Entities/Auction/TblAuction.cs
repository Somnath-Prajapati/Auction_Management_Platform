    using System;
    using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AuctionManagementSystem.Domain.Entities.Bids;
using AuctionManagementSystem.Domain.Entities.Asset;
namespace AuctionManagementSystem.Domain.Entities.Auction;

    public partial class TblAuction
    {
        public int AuctionId { get; set; }

        public string AuctionNumber { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string? Type { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int StatusId { get; set; }

        public int IncrementalTime { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? CreatedBy { get; set; }       
        public string? UpdatedBy { get; set; }       
        public string? DeletedBy { get; set; }       
        public DateTime? DeletedDate { get; set; }  
        public bool IsDeleted { get; set; }

         public int CategoryId { get; set; }
        [NotMapped]
        public decimal TotalPrice { get; set; }


        //[ForeignKey("CategoryId")]
     public virtual TblAssetCategory Category { get; set; } // added after modification

    public virtual TblAuctionStatus? Status { get; set; }

        public virtual ICollection<TblAuctionAsset> TblAuctionAssets { get; set; } = new List<TblAuctionAsset>();

        public virtual ICollection<TblAuctionView> TblAuctionViews { get; set; } = new List<TblAuctionView>();

        public virtual ICollection<tblBid> TblBids { get; set; } = new List<tblBid>();
    }
