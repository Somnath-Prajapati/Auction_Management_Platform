using System;
using System.Collections.Generic;
using AuctionManagementSystem.Domain.Entities.Transaction;

namespace AuctionManagementSystem.Domain.Entities;

public partial class TblCardType
{
    public int CardTypeId { get; set; }

    public string CardTypeName { get; set; } = null!;

    public virtual ICollection<TblTransaction> TblTransactions { get; set; } = new List<TblTransaction>();
}
