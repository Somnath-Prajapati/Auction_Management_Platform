using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Domain.Entities.Settings;

public partial class TblDirectSaleSetting
{
    public int Id { get; set; }

    public int? CartItemsLimit { get; set; }

    public int? CartTimerInMinutes { get; set; }
}
