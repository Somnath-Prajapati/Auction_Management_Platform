using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Domain.Entities;

public partial class TblRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<TblUserRole> TblUserRoles { get; set; } = new List<TblUserRole>();
}
