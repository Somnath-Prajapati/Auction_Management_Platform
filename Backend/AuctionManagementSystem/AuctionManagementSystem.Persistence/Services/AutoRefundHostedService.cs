using AuctionManagementSystem.Application.Features.Transactions.Commands.CreateTransaction;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;



namespace AuctionManagementSystem.Persistence.Services
{

    public class AutoRefundHostedService : BackgroundService
    {
        private readonly IMediator _mediator;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AutoRefundHostedService> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromSeconds(30); // run once daily

        public AutoRefundHostedService(IMediator mediator, ILogger<AutoRefundHostedService> logger, IServiceProvider serviceProvider)
        {
            _mediator = mediator;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    try
                    {
                        await mediator.Send(new ProcessAutoRefundCommand(), stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error occurred while processing auto refunds.");
                    }
                }

                // Wait for some time before running again
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }

}
