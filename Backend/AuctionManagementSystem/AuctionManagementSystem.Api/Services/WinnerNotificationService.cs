using AuctionManagementSystem.Api.Hubs;
using AuctionManagementSystem.Application.Contracts.RealTime;
using AuctionManagementSystem.Application.Dtos.Bids;
using Microsoft.AspNetCore.SignalR;

namespace AuctionManagementSystem.Api.Services
{
    public class WinnerNotificationService : IWinnerNotificationService
    {
        private readonly IHubContext<BidHub> _hubContext;
        public WinnerNotificationService(IHubContext<BidHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task NotifyWinnerList(List<AuctionWinnerDto> winners)
        {
            await _hubContext.Clients.All.SendAsync("WinnersList", winners);
        }
    }
}
