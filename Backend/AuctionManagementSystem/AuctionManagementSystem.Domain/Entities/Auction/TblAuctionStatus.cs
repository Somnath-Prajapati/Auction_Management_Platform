using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Domain.Entities.Auction;

public partial class TblAuctionStatus
{
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;
    public virtual ICollection<TblAuction> TblAuctions { get; set; } = new List<TblAuction>();
}
