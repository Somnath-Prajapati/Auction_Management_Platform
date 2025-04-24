using System;
using System.Collections.Generic;
using AuctionManagementSystem.Domain.Entities;
using AuctionManagementSystem.Domain.Entities.Asset;

namespace AuctionManagementSystem.Domain.Entities.Transaction;

public partial class TblTransactionAsset
{
    public int TransactionAssetsId { get; set; }

    public int TransactionId { get; set; }

    public int AssetId { get; set; }

    public decimal? BidPrice { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual TblAsset Asset { get; set; } = null!;

    public virtual TblTransaction Transaction { get; set; } = null!;
}
