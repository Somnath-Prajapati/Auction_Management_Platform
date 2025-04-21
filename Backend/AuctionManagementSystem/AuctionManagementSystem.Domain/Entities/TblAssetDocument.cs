using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Persistence.TempEntities;

public partial class TblAssetDocument
{
    public int DocumentId { get; set; }

    public int? AssetId { get; set; }

    public string? DocumentType { get; set; }

    public string? FilePath { get; set; }

    public virtual TblAsset? Asset { get; set; }
}
