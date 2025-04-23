using System;
using System.Collections.Generic;
using AuctionManagementSystem.Domain.Entities.Asset;

namespace AuctionManagementSystem.Domain.Entities;

public partial class TblVatoption
{
    public int Vatid { get; set; }

    public string Vattype { get; set; } = null!;

    public virtual ICollection<TblAsset> TblAssets { get; set; } = new List<TblAsset>();
}
