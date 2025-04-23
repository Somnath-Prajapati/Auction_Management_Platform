using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Domain.Entities.Asset;

public partial class TblAssetDetail
{
    public int DetailId { get; set; }

    public int? AssetId { get; set; }

    public string? AttributeName { get; set; }

    public string? AttributeValue { get; set; }

    public virtual TblAsset? Asset { get; set; }
}
