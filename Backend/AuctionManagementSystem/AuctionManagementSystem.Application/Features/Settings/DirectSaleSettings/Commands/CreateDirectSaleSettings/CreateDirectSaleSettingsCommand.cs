using AuctionManagementSystem.Application.Dtos.Settings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.DirectSaleSettings.Commands.CreateDirectSaleSettings
{
    public record CreateDirectSaleSettingsCommand : IRequest<DirectSaleSettingsDto>
    {
        public int CartItemsLimit { get; set; }
        public int CartTimerInMinutes { get; set; }
        public decimal MinimumBidAmount { get; set; }
        public decimal MinimumReservePrice { get; set; }
        public decimal MaximumDiscountAmount { get; set; }
    }
}
