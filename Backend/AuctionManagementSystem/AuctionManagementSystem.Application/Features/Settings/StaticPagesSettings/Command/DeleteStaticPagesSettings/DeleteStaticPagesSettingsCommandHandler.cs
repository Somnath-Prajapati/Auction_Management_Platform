using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Application.Interfaces.Repositories;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Command.DeleteStaticPagesSettings
{
    public class DeleteStaticPagesSettingsCommandHandler : IRequestHandler<DeleteStaticPagesSettingsCommand, bool>
    {
        private readonly IStaticPagesSettingsRepository _repository;

        public DeleteStaticPagesSettingsCommandHandler(IStaticPagesSettingsRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteStaticPagesSettingsCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return false;

            await _repository.DeleteAsync(entity);
            return true;
        }
    }
}
