using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Domain.Entities.Request;

public partial class TblRequestStatusHistory
{
    public int StatusHistoryId { get; set; }

    public int RequestId { get; set; }

    public int? PreviousRequestStatusId { get; set; }

    public int NewRequestStatusId { get; set; }

    public int ChangedByAdminId { get; set; }

    public DateTime ChangeDateTime { get; set; }

    public string? Note { get; set; }

    public virtual TblRequestStatus NewRequestStatus { get; set; } = null!;

    public virtual TblRequestStatus? PreviousRequestStatus { get; set; }

    public virtual TblRequest Request { get; set; } = null!;
}
