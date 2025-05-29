using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Contracts.AuditTrail
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        string Username { get; }
        int RoleId { get; }
        string RoleName { get; }
    }

}
