using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Domain.Entities.Transaction;

public partial class TblTransactionStatus
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<TblTransaction> TblTransactions { get; set; } = new List<TblTransaction>();
}
