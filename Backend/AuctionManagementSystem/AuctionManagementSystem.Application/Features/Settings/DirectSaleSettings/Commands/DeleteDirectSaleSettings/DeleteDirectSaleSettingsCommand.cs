using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.DirectSaleSettings.Commands.DeleteDirectSaleSettings
{
    public record DeleteDirectSaleSettingCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteDirectSaleSettingCommand(int id) { Id = id; }
    }



}