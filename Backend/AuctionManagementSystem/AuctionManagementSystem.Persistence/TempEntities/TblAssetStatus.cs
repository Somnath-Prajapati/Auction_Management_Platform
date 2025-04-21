using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Persistence.TempEntities;

public partial class TblAssetStatus
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<TblAsset> TblAssets { get; set; } = new List<TblAsset>();
}
