using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Domain.Entities.Request;

public partial class TblRequestStatus
{
    public int RequestStatusId { get; set; }

    public int RequestTypeId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual TblRequestType RequestType { get; set; } = null!;

    public virtual ICollection<TblRequestStatusHistory> TblRequestStatusHistoryNewRequestStatuses { get; set; } = new List<TblRequestStatusHistory>();

    public virtual ICollection<TblRequestStatusHistory> TblRequestStatusHistoryPreviousRequestStatuses { get; set; } = new List<TblRequestStatusHistory>();

    public virtual ICollection<TblRequest> TblRequests { get; set; } = new List<TblRequest>();
}
