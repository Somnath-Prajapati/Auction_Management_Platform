using AuctionManagementSystem.Application.Dtos.Settings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.DirectSaleSettings.Queries.GetAllDirectSaleSettings
{
    public record GetAllDirectSaleSettingsQuery : IRequest<List<DirectSaleSettingsDto>> { }
}
