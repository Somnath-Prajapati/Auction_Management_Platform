using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.AuditTrail;

namespace AuctionManagementSystem.Application.Contracts.AuditTrail
{
    public interface IAuditTrailRepository
{
    Task<List<TblAuditTrail>> GetAllAsync();
}

}
