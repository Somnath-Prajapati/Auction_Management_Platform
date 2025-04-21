using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Domain.Entities;

public partial class TblSeller
{
    public int SellerId { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<TblAsset> TblAssets { get; set; } = new List<TblAsset>();

    public virtual TblUser? User { get; set; }
}
