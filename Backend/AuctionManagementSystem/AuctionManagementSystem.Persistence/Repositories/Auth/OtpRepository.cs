using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Domain.Entities.User;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace AuctionManagementSystem.Identity.Repository
{
    public class OtpRepository : IOtpRepository
    {
            private readonly AuctionManagementDbContext _context;

            public OtpRepository(AuctionManagementDbContext context)
            {
                _context = context;
            }

            public async Task<tblOTP?> GetLatestOtpByUserIdAsync(int userId)
            {
                return await _context.tblOTPs
                    .Where(o => o.UserId == userId)
                    .OrderByDescending(o => o.Expiration)
                    .FirstOrDefaultAsync();
            }

            public async Task AddOtpAsync(tblOTP otp)
            {
                await _context.tblOTPs.AddAsync(otp);
            }

            public async Task SaveChangesAsync()
            {
                await _context.SaveChangesAsync();
            }
            public async Task<tblOTP?> GetValidOtpAsync(int userId, string code)
            {
                return await _context.tblOTPs
                    .FirstOrDefaultAsync(o => o.UserId == userId &&
                                              o.Code == code &&
                                              !o.IsUsed &&
                                              o.Expiration > DateTime.UtcNow);
            }
            public async Task Delete(tblOTP otp)
            {
                _context.tblOTPs.Remove(otp);
            }
    }
}

