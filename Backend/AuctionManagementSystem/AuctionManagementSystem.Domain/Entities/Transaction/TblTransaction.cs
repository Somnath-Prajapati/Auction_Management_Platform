using System;
using System.Collections.Generic;
using AuctionManagementSystem.Domain.Entities;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Request;
using AuctionManagementSystem.Domain.Entities.User;

namespace AuctionManagementSystem.Domain.Entities.Transaction;



public partial class TblTransaction
{
    public int TransactionId { get; set; }

    public string TransactionNumber { get; set; } = null!;

    public decimal Amount { get; set; }

    public int UserId { get; set; }

    public int TransactionTypeId { get; set; }

    public int PaymentMethodId { get; set; }

    public int? CardTypeId { get; set; }

    public string? MerchantTransactionId { get; set; }

    public DateTime TransactionDateTime { get; set; }

    public int StatusId { get; set; }

    public string? Notes { get; set; }

    public string? DocumentPath { get; set; }

    public long? CreatedByAdminId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    // 🔽 New audit fields
    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool IsDeleted { get; set; }

    // Navigation properties
    public virtual TblCardType? CardType { get; set; }

    public virtual TblPaymentMethod PaymentMethod { get; set; } = null!;

    public virtual TblTransactionStatus Status { get; set; } = null!;

    public virtual ICollection<TblRequest> TblRequests { get; set; } = new List<TblRequest>();

    public virtual ICollection<TblTransactionAsset> TblTransactionAssets { get; set; } = new List<TblTransactionAsset>();

    public virtual ICollection<TblTransactionDocument> TblTransactionDocuments { get; set; } = new List<TblTransactionDocument>();
    public virtual ICollection<TblOrder> TblOrders { get; set; } = new List<TblOrder>();

    public virtual TblTransactionType TransactionType { get; set; } = null!;

    public virtual TblUser User { get; set; } = null!;
}