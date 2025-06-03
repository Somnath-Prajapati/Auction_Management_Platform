using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Contracts.Bids
{
    public interface IAuctionJobScheduler
    {
        string ScheduleAuctionClosing(int auctionId, DateTimeOffset runAt);
        void CancelScheduledAuctionClosing(string hangfireJobId);
    }

}
