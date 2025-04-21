using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Persistence.TempEntities;

public partial class TblAssetWinner
{
    public int WinnerId { get; set; }

    public int? AssetId { get; set; }

    public int? UserId { get; set; }

    public decimal? AwardedPrice { get; set; }

    public string? Reason { get; set; }

    public string? Note { get; set; }

    public bool? Approved { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual TblAsset? Asset { get; set; }

    public virtual ICollection<TblWinnerDocument> TblWinnerDocuments { get; set; } = new List<TblWinnerDocument>();

    public virtual TblUser? User { get; set; }
}
