using AuctionManagementSystem.Application.Dtos.Settings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.UpdateFinanceSettings
{
    public record UpdateFinanceSettingsCommand : IRequest<FinanceSettingsDto>
    {
        public int Id { get; set; }
        public FinanceSettingsDto FinanceSettings { get; set; }
    }
}
