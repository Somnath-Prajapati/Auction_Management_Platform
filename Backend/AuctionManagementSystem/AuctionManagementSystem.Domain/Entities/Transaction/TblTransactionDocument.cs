using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Domain.Entities.Transaction;

public partial class TblTransactionDocument
{
    public int DocumentId { get; set; }

    public int TransactionId { get; set; }

    public string? DocumentType { get; set; }

    public string FilePath { get; set; } = null!;

    public DateTime? UploadedAt { get; set; }

    public virtual TblTransaction Transaction { get; set; } = null!;
}
