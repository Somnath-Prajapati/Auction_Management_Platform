using System;
using System.Collections.Generic;
using AuctionManagementSystem.Domain.Entities.Asset;

namespace AuctionManagementSystem.Domain.Entities;

public partial class TblWinnerDocument
{
    public int WinnerDocumentId { get; set; }

    public int? WinnerId { get; set; }

    public string? DocumentType { get; set; }

    public string? FilePath { get; set; }

    public DateTime? UploadedAt { get; set; }

    public virtual TblAssetWinner? Winner { get; set; }
}
