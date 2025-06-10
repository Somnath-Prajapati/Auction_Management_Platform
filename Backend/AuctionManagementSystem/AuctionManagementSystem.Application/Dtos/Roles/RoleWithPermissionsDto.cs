using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Roles
{
    public class RoleWithPermissionsDto
    {
        public int RoleId { get; set; } 
        public string RoleName { get; set; } 
        public bool IsSeller { get; set; } 


        // Permissions flattened for frontend
        public bool SuperAdmin { get; set; } = false;
        public bool AccessAdminPanel { get; set; } = false;
        public bool ManageAuctions { get; set; } = false;
        public bool ManageAssets { get; set; } = false;
        public bool ManageTransactions { get; set; } = false;
        public bool ManageCategories { get; set; } = false;
        public bool ManageRoles { get; set; } = false;
        public bool ManageUsers { get; set; } = false;
        public bool ViewReports { get; set; } = false;
        public bool ExportReports { get; set; } = false; 
        public bool ManageRequests { get; set; } = false;
        public bool ViewAuditTrail { get; set; } = false;
        public bool ChangeCommission { get; set; } = false;
    }
}


