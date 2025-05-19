using System;
using System.Collections.Generic;
using AuctionManagementSystem.Domain.Entities.Transaction;
using AuctionManagementSystem.Domain.Entities.User;

namespace AuctionManagementSystem.Domain.Entities.Asset;

public partial class TblOrder
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public string OrderStatus { get; set; } = null!;

    public int? TransactionId { get; set; }

    public string? TransactionNumber { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string? DeletedBy { get; set; }

    public virtual ICollection<TblOrderAsset> TblOrderAssets { get; set; } = new List<TblOrderAsset>();

    public virtual TblTransaction? Transaction { get; set; }

    public virtual TblUser User { get; set; } = null!;


}