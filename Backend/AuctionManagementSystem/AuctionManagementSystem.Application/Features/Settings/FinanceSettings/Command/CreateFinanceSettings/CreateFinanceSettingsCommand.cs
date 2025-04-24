using AuctionManagementSystem.Application.Dtos.Settings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.CreateFinanceSettings
{
    public record CreateFinanceSettingsCommand : IRequest<FinanceSettingsDto>
    {
        public FinanceSettingsDto FinanceSettings { get; set; }
    }
}
