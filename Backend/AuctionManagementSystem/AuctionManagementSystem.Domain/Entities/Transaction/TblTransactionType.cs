using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Domain.Entities.Transaction;

public partial class TblTransactionType
{
    public int TransactionTypeId { get; set; }

    public string TransactionTypeName { get; set; } = null!;

    public virtual ICollection<TblTransaction> TblTransactions { get; set; } = new List<TblTransaction>();
}
