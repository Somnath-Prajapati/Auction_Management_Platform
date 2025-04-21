using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Persistence.TempEntities;

public partial class TblFinanceSetting
{
    public int Id { get; set; }

    public decimal? Vatpercent { get; set; }

    public decimal? CreditCardFee { get; set; }

    public decimal? DebitCardFee { get; set; }

    public decimal? AdminFees { get; set; }

    public decimal? AuctionFees { get; set; }

    public decimal? BuyerCommissionPercent { get; set; }
}
