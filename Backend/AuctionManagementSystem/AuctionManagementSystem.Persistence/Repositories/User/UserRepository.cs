using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Domain.Entities.User;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Persistence.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly AuctionManagementDbContext _context;

        public UserRepository(AuctionManagementDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> AddUserAsync(TblUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await _context.TblUsers.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<bool> DeleteUserAsync(TblUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _context.TblUsers.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<TblUser>> GetAllUsersAsync()
        {
            return await _context.TblUsers
                .Where(u => !u.IsDeleted ?? false)
                .OrderByDescending(u => u.UpdatedDate ?? u.CreatedDate)
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
            if (tblUser == null)
                throw new ArgumentNullException(nameof(tblUser));

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
            // Validate inputs - both cannot be null or empty
            if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(mobileNumber))
            {
                throw new ArgumentException("Either email or mobile number must be provided");
            }

            // Build query safely handling nulls
            var query = _context.TblUsers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(mobileNumber))
            {
                // Both email and mobile are provided
                return await query.FirstOrDefaultAsync(u => u.Email == email || u.MobileNumber == mobileNumber);
            }
            else if (!string.IsNullOrWhiteSpace(email))
            {
                // Only email is provided
                return await query.FirstOrDefaultAsync(u => u.Email == email);
            }
            else
            {
                // Only mobile number is provided
                return await query.FirstOrDefaultAsync(u => u.MobileNumber == mobileNumber);
            }
        }

        public async Task<TblUser?> GetByPersonalIdNumberAsync(string personalIdNumber)
        {
            if (string.IsNullOrWhiteSpace(personalIdNumber))
                return null;

            return await _context.TblUsers
                .FirstOrDefaultAsync(u => u.PersonalIdNumber == personalIdNumber);
        }

        public async Task<TblUser?> GetByEmailOrMobileForUpdateAsync(string email, string mobileNumber, int excludeUserId)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(mobileNumber))
            {
                throw new ArgumentException("Either email or mobile number must be provided");
            }

            var query = _context.TblUsers.Where(u => u.UserId != excludeUserId);

            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(mobileNumber))
            {
                return await query.FirstOrDefaultAsync(u => u.Email == email || u.MobileNumber == mobileNumber);
            }
            else if (!string.IsNullOrWhiteSpace(email))
            {
                return await query.FirstOrDefaultAsync(u => u.Email == email);
            }
            else
            {
                return await query.FirstOrDefaultAsync(u => u.MobileNumber == mobileNumber);
            }
        }

        public async Task<TblUser?> GetByPersonalIdNumberForUpdateAsync(string personalIdNumber, int excludeUserId)
        {
            if (string.IsNullOrWhiteSpace(personalIdNumber))
                return null;

            return await _context.TblUsers
                .FirstOrDefaultAsync(u =>
                    u.PersonalIdNumber == personalIdNumber &&
                    u.UserId != excludeUserId);
        }

        public async Task<TblUser?> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            return await _context.TblUsers.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}