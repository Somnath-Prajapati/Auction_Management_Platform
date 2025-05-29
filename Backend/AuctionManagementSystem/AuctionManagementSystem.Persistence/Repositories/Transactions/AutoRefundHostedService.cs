using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Features.Transactions.Commands.CreateTransaction;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;



namespace AuctionManagementSystem.Persistence.Repositories.Transactions
{
   
    public class AutoRefundHostedService : BackgroundService
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AutoRefundHostedService> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromHours(24); // run once daily

        public AutoRefundHostedService(IMediator mediator, ILogger<AutoRefundHostedService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("AutoRefundHostedService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    int processedCount = await _mediator.Send(new ProcessAutoRefundCommand(), stoppingToken);
                    _logger.LogInformation($"Auto refund processed {processedCount} refund(s).");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while processing auto refunds.");
                }

                await Task.Delay(_interval, stoppingToken);
            }

            _logger.LogInformation("AutoRefundHostedService stopping.");
        }
    }


}
