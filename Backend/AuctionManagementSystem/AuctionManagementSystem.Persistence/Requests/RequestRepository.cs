using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Request;
using AuctionManagementSystem.Domain.Entities.Request;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

//using Azure.Core;

namespace AuctionManagementSystem.Persistence.Repositories.Requests
{
    public class RequestRepository : IRequestRepository
    {
        private readonly AuctionManagementDbContext _context;

        public RequestRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TblRequest>> GetAllRequestQuery()
        {
            return await _context.TblRequests
                
                .ToListAsync();
        }

        public async Task<TblRequest> GetRequestByIdQuery(int requestId)
        {
            return await _context.TblRequests.FindAsync(requestId);
        }

        public async Task AddRequest(TblRequest request)
        {
            await _context.TblRequests.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRequest(TblRequest request)
        {
            _context.TblRequests.Update(request);
            await _context.SaveChangesAsync();
        }

        public async Task DelRequest(TblRequest request)
        {
            _context.TblRequests.Remove(request);
            await _context.SaveChangesAsync();
        }
    }
}