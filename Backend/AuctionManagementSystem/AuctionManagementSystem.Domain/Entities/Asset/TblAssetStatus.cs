using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Domain.Entities.Asset;

public partial class TblAssetStatus
{
    public int StatusId { get; set; }

    public string StatusName { get; set; }

    public virtual ICollection<TblAssetCategory> TblAssetCategories { get; set; } = new List<TblAssetCategory>();

    public virtual ICollection<TblAsset> TblAssets { get; set; } = new List<TblAsset>();
}
