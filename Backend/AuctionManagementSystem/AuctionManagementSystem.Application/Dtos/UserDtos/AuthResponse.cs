using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.UserDtos
{
    public record AuthResponse(string Token, DateTime Expiration);
}
