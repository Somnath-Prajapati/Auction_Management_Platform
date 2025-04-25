using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Domain.Entities.Asset;

public partial class TblAssetDocument
{
    public int DocumentId { get; set; }

    public int? AssetId { get; set; }

    public string? DocumentType { get; set; }

    public string? FilePath { get; set; }

    public virtual TblAsset? Asset { get; set; }
}
    