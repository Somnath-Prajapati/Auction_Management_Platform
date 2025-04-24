using AuctionManagementSystem.Application.Dtos.Settings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.DirectSaleSettings.Queries.GetDirectSaleSettingsById
{
    public record GetDirectSaleSettingByIdQuery : IRequest<DirectSaleSettingsDto>
    {
        public int Id { get; set; }

        public GetDirectSaleSettingByIdQuery(int id)
        {
            Id = id;
        }
    }
}
