using AuctionManagementSystem.Application.Dtos.Settings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Query.GetStaticPagesSettingsById
{
    public record GetDirectSaleSettingByIdQuery(int Id) : IRequest<DirectSaleSettingsDto>;

}
