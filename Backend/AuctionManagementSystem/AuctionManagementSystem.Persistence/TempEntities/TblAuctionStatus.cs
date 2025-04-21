using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Persistence.TempEntities;

public partial class TblAuctionStatus
{
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;
}
