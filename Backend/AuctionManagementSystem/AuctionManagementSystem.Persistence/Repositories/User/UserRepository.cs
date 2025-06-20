using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.Notification;
using AuctionManagementSystem.Domain.Entities.Notification;
using AuctionManagementSystem.Domain.Entities.User;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly AuctionManagementDbContext _context;

        public UserRepository(AuctionManagementDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [AllowAnonymous]
        public async Task<int> AddUserAsync(TblUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await _context.TblUsers.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.UserId;
        }

        [AllowAnonymous]
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

        public async Task<TblUser?> GetUserById(int id)
        {
            return await _context.TblUsers
                .Where(u => u.UserId == id && !(u.IsDeleted ?? false))
                .FirstOrDefaultAsync();
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
            return await _context.TblUsers
                 .Where(u => !(u.IsDeleted ?? false))
                .FirstOrDefaultAsync(u => u.Email == email || u.MobileNumber == mobileNumber);
        }

        public async Task<TblUser?> GetByPersonalIdNumberAsync(string personalIdNumber)
        {
            if (string.IsNullOrWhiteSpace(personalIdNumber))
                return null;

            return await _context.TblUsers
                 .Where(u => !(u.IsDeleted ?? false))
                .FirstOrDefaultAsync(u => u.PersonalIdNumber == personalIdNumber);
        }

        public async Task<TblUser?> GetByEmailOrMobileForUpdateAsync(string email, string mobileNumber, int excludeUserId)
        {
            return await _context.TblUsers
                 .Where(u => !(u.IsDeleted ?? false))
                .FirstOrDefaultAsync(u =>
                    (u.Email == email || u.MobileNumber == mobileNumber) &&
                    u.UserId != excludeUserId);
        }

        public async Task<TblUser?> GetByPersonalIdNumberForUpdateAsync(string personalIdNumber, int excludeUserId)
        {
            if (string.IsNullOrWhiteSpace(personalIdNumber))
                return null;

            return await _context.TblUsers
                 .Where(u => !(u.IsDeleted ?? false))
                .FirstOrDefaultAsync(u =>
                    u.PersonalIdNumber == personalIdNumber &&
                    u.UserId != excludeUserId);
        }

        public async Task<TblUser?> GetUserByEmailAsync(string email)
        {
            return await _context.TblUsers.Where(u => !(u.IsDeleted ?? false)).FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<NotificationDto>> GetNotificationByUserId(int UserId, string langCode)
        {
            var now = DateTime.UtcNow;
            int languageId = langCode.ToLower() == "ar" ? 2 : 1;

            var query = from n in _context.TblNotifications
                        where !n.IsDeleted
                           && (n.UserId == UserId || n.UserId == null)
                           && (n.ExpiresAt == null || n.ExpiresAt > now)
                           && n.Title != "New User Added"
                        join t in _context.TblNotificationTranslations
                            .Where(t => t.LanguageId == languageId)
                            on n.NotificationId equals t.NotificationId into nt
                        from trans in nt.DefaultIfEmpty()
                        select new NotificationDto
                        {
                            Id = n.NotificationId,
                            UserId = n.UserId,
                            Title = trans != null && !string.IsNullOrEmpty(trans.Title) ? trans.Title : n.Title,
                            Message = trans != null && !string.IsNullOrEmpty(trans.Message) ? trans.Message : n.Message,
                            IsRead = n.IsRead,
                            CreatedAt = n.CreatedAt,
                            ExpiresAt = n.ExpiresAt,
                            AssetId = n.AssetId,
                            AuctionId = n.AuctionId
                        };

            return await query.ToListAsync();
        }




    }
}