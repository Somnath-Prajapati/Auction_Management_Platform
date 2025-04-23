using System;
using System.Collections.Generic;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Request;
using AuctionManagementSystem.Domain.Entities.Transaction;

namespace AuctionManagementSystem.Domain.Entities.User;

public partial class TblUser
{
    public int UserId { get; set; }

    public int Uid { get; set; }

    public string Name { get; set; } = null!;

    public string MobileNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

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

    public string PersonalIdNumber { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly PersonalIdExpiryDate { get; set; }

    public string? ProfileImage { get; set; }

    public int? CountryId { get; set; }

    public virtual TblCountry? Country { get; set; }

    public virtual TblUserStatus Status { get; set; } = null!;

    public virtual ICollection<TblAssetWinner> TblAssetWinners { get; set; } = new List<TblAssetWinner>();

    public virtual ICollection<TblSeller> TblSellers { get; set; } = new List<TblSeller>();

    public virtual ICollection<TblUserRole> TblUserRoles { get; set; } = new List<TblUserRole>();
    public virtual ICollection<TblRequest> TblRequests { get; set; } = new List<TblRequest>();

    public virtual ICollection<TblTransaction> TblTransactions { get; set; } = new List<TblTransaction>();


}
