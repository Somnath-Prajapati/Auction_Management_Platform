using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class SellerDto
    {
        public int SellerId { get; set; }

        public int? UserId { get; set; }

        public string? UserName { get; set; }

    }
}
