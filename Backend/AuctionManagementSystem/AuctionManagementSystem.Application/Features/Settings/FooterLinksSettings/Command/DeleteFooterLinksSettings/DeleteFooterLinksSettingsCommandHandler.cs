using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Application.Repositories;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Command.DeleteFooterLinksSettings
{
    public class DeleteFooterLinksSettingCommandHandler : IRequestHandler<DeleteFooterLinksSettingCommand, bool>
    {
        private readonly IFooterLinksSettingsRepository _repository;

        public DeleteFooterLinksSettingCommandHandler(IFooterLinksSettingsRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteFooterLinksSettingCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteAsync(request.Id);
        }
    }
}
