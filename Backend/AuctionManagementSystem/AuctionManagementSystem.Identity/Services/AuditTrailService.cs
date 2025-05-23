using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Domain.Entities.AuditTrail;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AuctionManagementSystem.Identity.Services
{
    public class AuditTrailService : IAuditTrailService
    {
        private readonly AuctionManagementDbContext _context;

        public AuditTrailService(AuctionManagementDbContext context)
        {
            _context = context;
        }

    public async Task LogChangeAsync(
    int userId,
    string username,
    string roleName, // remove roleId param
    string modelName,
    string changeType,
    int? recordId,
    object? beforeChange,
    object? afterChange)
        {
            Console.WriteLine($"Audit called for: {modelName} / {changeType} by {username}");

            var userExists = await _context.TblUsers.AnyAsync(u => u.UserId == userId);
            if (!userExists)
            {
                Console.WriteLine($"Audit trail skipped: UserId {userId} not found.");
                return;
            }

            // Look up RoleId from RoleName
            int roleId = await _context.TblRoles
                .Where(r => r.RoleName == roleName)
                .Select(r => r.RoleId)
                .FirstOrDefaultAsync();

            if (roleId == 0)
            {
                Console.WriteLine($"Audit warning: Role '{roleName}' not found, defaulting RoleId to 0.");
            }

            var audit = new TblAuditTrail
            {
                UserId = userId,
                RoleId = roleId,
                CreatedBy = username,
                CreatedDate = DateTime.UtcNow,
                ActivityPerformedAt = DateTime.UtcNow,
                IsDeleted = false,
                ModelName = modelName,
                ChangeType = changeType,
                RecordId = recordId,
                BeforeChange = beforeChange != null ? JsonConvert.SerializeObject(beforeChange) : null,
                AfterChange = afterChange != null ? JsonConvert.SerializeObject(afterChange) : null
            };

            _context.TblAuditTrails.Add(audit);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var inner = ex.InnerException?.Message;
                Console.WriteLine($"EF Save error: {inner}");
                throw;
            }
        }


    }

}
