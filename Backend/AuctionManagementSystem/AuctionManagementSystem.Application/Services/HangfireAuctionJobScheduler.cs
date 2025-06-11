using AuctionManagementSystem.Application.Contracts.Bids;
using Hangfire;

namespace AuctionManagementSystem.Application.Services
{
    public class HangfireAuctionJobScheduler : IAuctionJobScheduler
    {
        public string ScheduleAuctionClosing(int auctionId, DateTimeOffset runAt)
        {
            var jobId = BackgroundJob.Schedule<IAuctionWinnerService>(
                service => service.WinnerAuctionAsync(auctionId),
                runAt);
            return jobId;
        }

        public void CancelScheduledAuctionClosing(string hangfireJobId)
        {
            BackgroundJob.Delete(hangfireJobId);
        }
    }

}
