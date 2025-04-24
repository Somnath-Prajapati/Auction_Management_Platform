using AuctionManagementSystem.Application.Dtos.Settings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.DirectSaleSettings.Commands.UpdateDirectSaleSettings
{
    public record UpdateDirectSaleSettingsCommand : IRequest<DirectSaleSettingsDto>
    {
        public int Id { get; set; }
        public int CartItemsLimit { get; set; }
        public int CartTimerInMinutes { get; set; }
    }
}
