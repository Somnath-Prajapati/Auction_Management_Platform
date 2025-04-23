using AuctionManagementSystem.Application.Dtos.Settings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Query.GetFooterLinksSettingById
{
    public record GetFooterLinksSettingByIdQuery(int Id) : IRequest<FooterLinksSettingsDto>;
}
