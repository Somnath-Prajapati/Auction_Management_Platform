using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Models
{
    public record AuthResult(bool Success, string Token, string ErrorMessage = null);
}

