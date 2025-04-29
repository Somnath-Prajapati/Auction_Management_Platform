using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.UserDtos
{
    public record LoginRequest(string EmailOrPhone);
}
