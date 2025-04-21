using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Persistence.TempEntities;

public partial class TblAssetGallery
{
    public int GalleryId { get; set; }

    public int? AssetId { get; set; }

    public string? MediaType { get; set; }

    public string? FilePath { get; set; }

    public int? SortOrder { get; set; }

    public virtual TblAsset? Asset { get; set; }
}
