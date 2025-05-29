using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Domain.Entities.AuditTrail;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.AuditTrail
{
    public class AuditTrailRepository : IAuditTrailRepository
    {
        private readonly AuctionManagementDbContext _context;

        public AuditTrailRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<TblAuditTrail>> GetAllAsync()
        {
            return await _context.TblAuditTrails
                .Where(x => (bool)!x.IsDeleted)
                .OrderByDescending(x => x.ActivityPerformedAt)
                .ToListAsync();
        }
    }
}
