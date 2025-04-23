using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Domain.Entities.Request;

public partial class TblRequestType
{
    public int RequestTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<TblRequestStatus> TblRequestStatuses { get; set; } = new List<TblRequestStatus>();

    public virtual ICollection<TblRequest> TblRequests { get; set; } = new List<TblRequest>();
}
