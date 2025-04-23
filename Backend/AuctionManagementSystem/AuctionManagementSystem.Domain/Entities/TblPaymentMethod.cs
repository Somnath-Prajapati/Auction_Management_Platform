using System;
using System.Collections.Generic;
using AuctionManagementSystem.Domain.Entities.Transaction;

namespace AuctionManagementSystem.Domain.Entities;

public partial class TblPaymentMethod
{
    public int PaymentMethodId { get; set; }

    public string PaymentMethodName { get; set; } = null!;

    public virtual ICollection<TblTransaction> TblTransactions { get; set; } = new List<TblTransaction>();
}
