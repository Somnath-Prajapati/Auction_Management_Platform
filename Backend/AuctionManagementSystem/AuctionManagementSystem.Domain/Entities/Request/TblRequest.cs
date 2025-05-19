using System;
using System.Collections.Generic;
using AuctionManagementSystem.Domain.Entities;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Transaction;
using AuctionManagementSystem.Domain.Entities.User;

namespace AuctionManagementSystem.Domain.Entities.Request;

public partial class TblRequest
{
    public int RequestId { get; set; }

    public string RequestNumber { get; set; } = null!;

    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string MobileNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int RequestTypeId { get; set; }

    public int AssetId { get; set; }

    public int? TransactionId { get; set; }

    public DateTime RequestDateTime { get; set; }

    public int RequestStatusId { get; set; }

    public string? CustomerNote { get; set; }

    public string? AdminNote { get; set; }

    public bool CreatedByAdmin { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public bool IsDeleted { get; set; }

    public string? DeletedBy { get; set; }
    public DateTime? DeletedDate { get; set; }
    public virtual TblAsset Asset { get; set; } = null!;

    public virtual TblRequestStatus RequestStatus { get; set; } = null!;

    public virtual TblRequestType RequestType { get; set; } = null!;

    public virtual ICollection<TblRequestStatusHistory> TblRequestStatusHistories { get; set; } = new List<TblRequestStatusHistory>();

    public virtual TblTransaction? Transaction { get; set; }

    public virtual TblUser User { get; set; } = null!;

}
