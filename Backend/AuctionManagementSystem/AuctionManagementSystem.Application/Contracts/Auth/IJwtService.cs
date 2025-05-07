using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.User;

namespace AuctionManagementSystem.Application.Contracts.Auth
{
    public interface IJwtService
    {
        string GenerateToken(TblUser user, string roleName);
    }

}
