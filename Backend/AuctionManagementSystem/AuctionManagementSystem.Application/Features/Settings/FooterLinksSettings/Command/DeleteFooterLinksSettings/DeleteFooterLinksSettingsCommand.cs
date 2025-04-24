using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Command.DeleteFooterLinksSettings
{
    public record DeleteFooterLinksSettingCommand(int Id) : IRequest<bool>;
}
