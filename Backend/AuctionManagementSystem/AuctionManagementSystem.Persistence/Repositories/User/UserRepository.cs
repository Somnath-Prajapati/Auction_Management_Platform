using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Domain.Entities.User;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly AuctionManagementDbContext _context;
        public UserRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddUserAsync(TblUser user)
        {
            _context.TblUsers.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<bool> DeleteUserAsync(TblUser user)
        {
            _context.TblUsers.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<TblUser>> GetAllUsersAsync()
        {
            return await _context.TblUsers
                .Include(u => u.Status)
                .Include(u => u.Country)
                .ToListAsync();
        }

        public async Task<TblUser> GetUserById(int id)
        {
            return await _context.TblUsers.FindAsync(id);
        }

        public async Task<int> UpdateUserAsync(TblUser tblUser)
        {
            _context.TblUsers.Update(tblUser);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> GenerateNextUidAsync(int startFrom = 1003)
        {
            var maxUid = await _context.TblUsers.MaxAsync(u => (int?)u.Uid) ?? startFrom - 1;
            return maxUid + 1;
        }
        public async Task<TblUser?> GetByEmailOrMobileAsync(string email, string mobileNumber)
        {
            return await _context.TblUsers
                .FirstOrDefaultAsync(u => u.Email == email || u.MobileNumber == mobileNumber);
        }
    }
}
