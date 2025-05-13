using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Request;
<<<<<<< HEAD
=======
using AuctionManagementSystem.Application.Contracts.User;
>>>>>>> 856b2f7a75aef35d84c6e34f6bd4a53a17134313
using AuctionManagementSystem.Domain.Entities.Request;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

//using Azure.Core;

namespace AuctionManagementSystem.Persistence.Repositories.Requests
{
    public class RequestRepository : IRequestRepository
    {
        private readonly AuctionManagementDbContext _context;
<<<<<<< HEAD

        public RequestRepository(AuctionManagementDbContext context)
        {
            _context = context;
=======
        private readonly IUserRepository _userRepository;

        public RequestRepository(AuctionManagementDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
>>>>>>> 856b2f7a75aef35d84c6e34f6bd4a53a17134313
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

<<<<<<< HEAD
        public async Task AddRequest(TblRequest request)
        {
            await _context.TblRequests.AddAsync(request);
            await _context.SaveChangesAsync();
        }

=======
        public async Task<bool> RequestNumberExists(string requestNumber)
        {
            return await _context.TblRequests.AnyAsync(r => r.RequestNumber == requestNumber);
        }





        //public async Task AddRequest(TblRequest request)
        //{
        //    var get = await _userRepository.GetUserById(request.UserId);
        //    request.Username = get.Name;
        //    await _context.TblRequests.AddAsync(request);
        //    await _context.SaveChangesAsync();
        //}


        public async Task<string> GenerateRequestNumberAsync()
        {
            var today = DateTime.UtcNow;
            var datePart = today.ToString("yyyyMMdd");

            // Get last sequence used today
            var last = await _context.TblRequests
                .Where(r => r.RequestNumber.StartsWith($"REQ-{datePart}-"))
                .OrderByDescending(r => r.RequestNumber)
                .Select(r => r.RequestNumber)
                .FirstOrDefaultAsync();

            int nextSeq = 1;
            if (!string.IsNullOrEmpty(last))
            {
                var parts = last.Split('-');
                if (parts.Length == 3 && int.TryParse(parts[2], out var lastSeq))
                    nextSeq = lastSeq + 1;
            }

            // Determine padding
            var padLen = nextSeq > 999 ? 4 : 3;
            return $"REQ-{datePart}-{nextSeq.ToString().PadLeft(padLen, '0')}";
        }

        public async Task AddRequest(TblRequest request)
        {
            _context.TblRequests.Add(request);
            await _context.SaveChangesAsync();


        }

        public async Task DeleteRequestByNumberAsync(string requestNumber)
        {
            var entity = await _context.TblRequests
                .FirstOrDefaultAsync(r => r.RequestNumber == requestNumber);
            if (entity != null)
            {
                _context.TblRequests.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }


>>>>>>> 856b2f7a75aef35d84c6e34f6bd4a53a17134313
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
<<<<<<< HEAD
}
=======
}
>>>>>>> 856b2f7a75aef35d84c6e34f6bd4a53a17134313
