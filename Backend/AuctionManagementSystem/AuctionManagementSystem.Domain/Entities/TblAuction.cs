using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Persistence.TempEntities;

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

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CategoryId { get; set; }

    public virtual TblAuctionCategory? Category { get; set; }

    public virtual ICollection<TblAuctionAsset> TblAuctionAssets { get; set; } = new List<TblAuctionAsset>();

    public virtual ICollection<TblAuctionView> TblAuctionViews { get; set; } = new List<TblAuctionView>();
}
