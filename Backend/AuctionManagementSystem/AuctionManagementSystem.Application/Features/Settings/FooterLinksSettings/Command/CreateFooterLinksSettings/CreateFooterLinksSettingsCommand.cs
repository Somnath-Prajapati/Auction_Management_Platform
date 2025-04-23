using AuctionManagementSystem.Application.Dtos.Settings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Command.CreateFooterLinksSettings
{
    public class CreateFooterLinksSettingsCommand : IRequest<FooterLinksSettingsDto>
    {
        public FooterLinksSettingsDto FooterLinksSettingsDto { get; set; }  // Use DTO here

        public CreateFooterLinksSettingsCommand(FooterLinksSettingsDto footerLinksSettingsDto)
        {
            FooterLinksSettingsDto = footerLinksSettingsDto;
        }
    }
}
