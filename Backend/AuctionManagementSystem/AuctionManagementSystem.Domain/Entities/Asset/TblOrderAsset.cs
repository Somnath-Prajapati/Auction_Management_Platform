using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Domain.Entities.Asset;

public partial class TblOrderAsset
{
    public int OrderAssetId { get; set; }

    public int OrderId { get; set; }

    public int AssetId { get; set; }

    public virtual TblAsset Asset { get; set; } = null!;

    public virtual TblOrder Order { get; set; } = null!;
}
