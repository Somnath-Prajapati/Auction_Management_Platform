using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Listings
{
    public class AddToCartDto
    {
        public int UserId { get; set; }
        public int AssetId { get; set; }
    }
}
