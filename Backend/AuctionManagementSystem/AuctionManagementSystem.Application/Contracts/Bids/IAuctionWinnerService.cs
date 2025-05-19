using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Bids;

namespace AuctionManagementSystem.Application.Contracts.Bids
{
   public interface IAuctionWinnerService
    {
        Task<List<AuctionWinnerDto>> WinnerAuctionAsync(int auctionId);
    }

}
