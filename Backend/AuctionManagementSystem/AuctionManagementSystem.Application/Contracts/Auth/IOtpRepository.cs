using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.User;
using static System.Net.WebRequestMethods;

namespace AuctionManagementSystem.Application.Contracts.Auth
{
    public interface IOtpRepository
    {
        Task<tblOTP?> GetLatestOtpByUserIdAsync(int userId);
        Task AddOtpAsync(tblOTP otp);
        Task SaveChangesAsync();
        Task<tblOTP?> GetValidOtpAsync(int userId, string code);
        Task Delete(tblOTP otp);

    }

}
