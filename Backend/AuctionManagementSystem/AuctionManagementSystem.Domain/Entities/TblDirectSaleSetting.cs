using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Persistence.TempEntities;

public partial class TblDirectSaleSetting
{
    public int Id { get; set; }

    public int? CartItemsLimit { get; set; }

    public int? CartTimerInMinutes { get; set; }
}
