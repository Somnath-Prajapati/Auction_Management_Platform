using System;
using System.Collections.Generic;
using AuctionManagementSystem.Domain.Entities.AuditTrail;
using AuctionManagementSystem.Domain.Entities.Roles;

namespace AuctionManagementSystem.Domain.Entities.User;

public partial class TblRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;
    public bool IsSeller { get; set; }
    //public List<string> Permissions { get; set; } = new();

    public virtual TblRolePermissionsMatrix TblRolePermissionsMatrix { get; set; }
    public virtual ICollection<TblAuditTrail> TblAuditTrails { get; set; } = new List<TblAuditTrail>();


    public virtual ICollection<TblUserRole> TblUserRoles { get; set; } = new List<TblUserRole>();
}
