using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Command.DeleteStaticPagesSettings
{
    public class DeleteStaticPagesSettingsCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteStaticPagesSettingsCommand(int id)
        {
            Id = id;
        }
    }
}
