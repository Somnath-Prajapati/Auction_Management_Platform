using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class CartResponseDto
    {
        public List<DirectSaleAssetDto> Items { get; set; } = new();
        public string Message { get; set; }
        public List<string> ExpiredAssetTitles { get; set; } = new();
    }

}
