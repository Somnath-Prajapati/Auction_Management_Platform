using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Domain.Entities.User;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.User
{
    public class UserStatusRepository : IUserStatusRepository
    {
        private readonly AuctionManagementDbContext _context;

        public UserStatusRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TblUserStatus>> GetAllAsync()
        {
            return await _context.TblUserStatuses.ToListAsync();
        }
    }
}