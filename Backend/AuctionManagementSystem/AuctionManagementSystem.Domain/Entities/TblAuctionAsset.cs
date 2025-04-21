using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Persistence.TempEntities;

public partial class TblAuctionAsset
{
    public int AuctionAssetId { get; set; }

    public int AuctionId { get; set; }

    public int AssetId { get; set; }

    public virtual TblAsset Asset { get; set; } = null!;

    public virtual TblAuction Auction { get; set; } = null!;
}
