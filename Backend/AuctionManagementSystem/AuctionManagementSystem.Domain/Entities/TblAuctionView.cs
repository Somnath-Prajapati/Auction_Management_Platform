using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Domain.Entities;

public partial class TblAuctionView
{
    public int AuctionViewId { get; set; }

    public int AuctionId { get; set; }

    public Guid? UserId { get; set; }

    public DateTime? ViewedAt { get; set; }

    public virtual TblAuction Auction { get; set; } = null!;
}
