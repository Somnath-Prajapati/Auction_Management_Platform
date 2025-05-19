using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;

namespace AuctionManagementSystem.Application.Dtos.Listings
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string TransactionNumber { get; set; } = string.Empty;
        public string OrderStatus { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<DirectSaleAssetDto> Assets { get; set; } = new();
    }

   

}
