using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionManagementSystem.Domain.Entities.Roles
{
    

    public partial class TblRolePermissionsMatrix
    {
        public int RoleId { get; set; }

        [Column("Super Admin")]
        public bool? SuperAdmin { get; set; }

        [Column("Access Admin Panel")]
        public bool? AccessAdminPanel { get; set; }

        [Column("Manage Auctions")]
        public bool? ManageAuctions { get; set; }

        [Column("Manage Assets")]
        public bool? ManageAssets { get; set; }

        [Column("Manage Transactions")]
        public bool? ManageTransactions { get; set; }

        [Column("Manage Categories")]
        public bool? ManageCategories { get; set; }

        [Column("Manage Roles")]
        public bool? ManageRoles { get; set; }

        [Column("Manage Users")]
        public bool? ManageUsers { get; set; }

        [Column("View Reports")]
        public bool? ViewReports { get; set; }

        [Column("Export Reports")]
        public bool? ExportReports { get; set; }

        [Column("Manage Requests")]
        public bool? ManageRequests { get; set; }

        [Column("View Audit Trail")]
        public bool? ViewAuditTrail { get; set; }

        [Column("Change Commission")]
        public bool? ChangeCommission { get; set; }

        public virtual TblRole? Role { get; set; }


        public int TblRolePermissionsMatrixId { get; set; }
    }
}
