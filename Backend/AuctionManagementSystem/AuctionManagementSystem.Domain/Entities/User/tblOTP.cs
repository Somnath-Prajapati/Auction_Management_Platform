using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Domain.Entities.User
{
    public class tblOTP
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public bool IsUsed { get; set; }

        public TblUser User { get; set; } = null!;
    }

}
