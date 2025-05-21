using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Bids;

namespace AuctionManagementSystem.Application.Contracts.RealTime
{
    public interface IWinnerNotificationService
    {
        Task NotifyWinnerList(List<AuctionWinnerDto> winners);
    }
}
