using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Domain.Entities;

public partial class TblWinnerAwardingOption
{
    public int AwardingId { get; set; }

    public string AwardingMethod { get; set; } = null!;

    public virtual ICollection<TblAsset> TblAssets { get; set; } = new List<TblAsset>();
}
