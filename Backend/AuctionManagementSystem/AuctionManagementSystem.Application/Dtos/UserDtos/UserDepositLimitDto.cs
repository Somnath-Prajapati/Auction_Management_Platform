using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.UserDtos
{
    public class UserDepositLimitDto
    {
        public decimal TotalLimit { get; set; }
        public decimal CurrentDeposit { get; set; }
        public decimal AvailableLimit { get; set; }
    }

}
