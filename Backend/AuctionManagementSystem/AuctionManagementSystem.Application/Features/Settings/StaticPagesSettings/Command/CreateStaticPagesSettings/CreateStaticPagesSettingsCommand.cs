using System.Threading.Tasks;
using MediatR;
using AuctionManagementSystem.Application.Dtos.Settings;

namespace AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Command.CreateStaticPagesSettings
{
    public class CreateStaticPagesSettingsCommand : IRequest<StaticPagesSettingsDto>
    {
        public StaticPagesSettingsDto SettingsDto { get; set; } = new();
    }
}
