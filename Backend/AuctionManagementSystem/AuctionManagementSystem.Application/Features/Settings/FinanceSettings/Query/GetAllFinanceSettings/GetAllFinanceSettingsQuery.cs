using AuctionManagementSystem.Application.Dtos.Settings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Query.GetAllFinanceSettings
{
    public record GetAllFinanceSettingsQuery : IRequest<List<FinanceSettingsDto>> { }
}
