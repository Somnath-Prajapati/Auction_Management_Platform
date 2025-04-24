using AuctionManagementSystem.Application.Dtos.Settings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Command.UpdateStaticPagesSettings
{
    public class UpdateStaticPagesSettingsCommand : IRequest<StaticPagesSettingsDto>
    {
        public int Id { get; set; }
        public StaticPagesSettingsDto SettingsDto { get; set; } = new();
    }
}
