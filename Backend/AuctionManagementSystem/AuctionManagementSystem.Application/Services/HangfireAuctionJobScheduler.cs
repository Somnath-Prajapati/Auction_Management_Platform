using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Bids;
using Hangfire;

namespace AuctionManagementSystem.Application.Services
{
    public class HangfireAuctionJobScheduler : IAuctionJobScheduler
    {
        public void ScheduleAuctionClosing(int AuctionId, DateTimeOffset runAt)
        {
            BackgroundJob.Schedule<IAuctionWinnerService>(
            service => service.WinnerAuctionAsync(AuctionId),
            runAt);
        }
    }
}
