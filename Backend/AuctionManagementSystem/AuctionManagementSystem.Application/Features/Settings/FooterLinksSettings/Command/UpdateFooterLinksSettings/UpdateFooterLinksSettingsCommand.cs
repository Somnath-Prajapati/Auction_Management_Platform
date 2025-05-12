using AuctionManagementSystem.Application.Dtos.Settings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Command.UpdateFooterLinksSettings
{
    public class UpdateFooterLinksSettingsCommand : IRequest<FooterLinksSettingsDto>
    {
        //public int Id { get; set; }
        public FooterLinksSettingsDto FooterLinksSettings { get; set; } // Use the DTO for the properties
    }
}
