using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Contracts.AuditTrail
{
    public interface IAuditTrailService
    {
       Task LogChangeAsync(
       int userId,
       string username,
       string roleName, 
       string modelName,
       string changeType,
       int? recordId,
       object? beforeChange,
       object? afterChange);
    }

}
