using AuctionManagementSystem.Application.Dtos.Settings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Query.GetFinanceSettingsById
{
    public record GetFinanceSettingsByIdQuery : IRequest<FinanceSettingsDto>
    {
        public int Id { get; set; }

        public GetFinanceSettingsByIdQuery(int id)
        {
            Id = id;
        }
    }
}
