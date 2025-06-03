using System;
using System.Collections.Generic;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.AuditTrail;
using AuctionManagementSystem.Domain.Entities.Bids;
using AuctionManagementSystem.Domain.Entities.Notification;
using AuctionManagementSystem.Domain.Entities.Request;
using AuctionManagementSystem.Domain.Entities.Transaction;

namespace AuctionManagementSystem.Domain.Entities.User;



public partial class TblUser
{
    public int UserId { get; set; }

    public int Uid { get; set; }

    public string Name { get; set; }

    public string MobileNumber { get; set; }

    public string Email { get; set; }

    public string? CompanyName { get; set; }

    public string? CompanyNumber { get; set; }

    public int StatusId { get; set; }

    public bool ChatEnabled { get; set; }

    public int RoleId { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public DateTime? LastOnline { get; set; }

    public decimal? TotalLimit { get; set; }

    public decimal? Deposit { get; set; }

    public string? PersonalIdImage { get; set; }

    public string? PersonalIdNumber { get; set; }

    public string Gender { get; set; }

    public DateOnly? PersonalIdExpiryDate { get; set; }
   


    public string? ProfileImage { get; set; }

    public int? CountryId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsDeleted { get; set; }
    public decimal AvailableLimit { get; set; }

    public virtual TblCountry? Country { get; set; }

    public virtual TblUserStatus? Status { get; set; }

    public virtual ICollection<TblAssetWinner?> TblAssetWinners { get; set; } = new List<TblAssetWinner>();

    public virtual ICollection<TblRequest?> TblRequests { get; set; } = new List<TblRequest>();

    public virtual ICollection<TblSeller?> TblSellers { get; set; } = new List<TblSeller>();

    public virtual ICollection<tblBid> TblBids { get; set; } = new List<tblBid>();
    public virtual ICollection<TblTransaction?> TblTransactions { get; set; } = new List<TblTransaction>();
    public virtual ICollection<tblOTP?> OTPs { get; set; } = new List<tblOTP>();
    public virtual ICollection<TblOrder> TblOrders { get; set; } = new List<TblOrder>();

    public virtual ICollection<TblUserRole?> TblUserRoles { get; set; } = new List<TblUserRole>();
    public virtual ICollection<TblAuditTrail> TblAuditTrails { get; set; } = new List<TblAuditTrail>();

    public virtual ICollection<TblWishlistItem> TblWishlistItems { get; set; } = new List<TblWishlistItem>();
    public virtual ICollection<TblCartItem> TblCartItems { get; set; } = new List<TblCartItem>();

    // after the autobid added 
    public virtual ICollection<TblAutoBid> TblAutoBids { get; set; } = new List<TblAutoBid>();

    public virtual ICollection<TblNotification> TblNotifications { get; set; }
    public TblUser Clone()
    {
        return (TblUser)this.MemberwiseClone();
    }
}
