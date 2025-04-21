using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Persistence.TempEntities;

public partial class TblUserRole
{
    public int UserRoleId { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public DateTime? AssignedAt { get; set; }

    public virtual TblRole Role { get; set; } = null!;

    public virtual TblUser User { get; set; } = null!;
}
