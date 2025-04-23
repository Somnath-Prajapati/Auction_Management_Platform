using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.DeleteFinanceSettings
{
    public record DeleteFinanceSettingsCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteFinanceSettingsCommand(int id)
        {
            Id = id;
        }
    }
}
