using AuctionManagementSystem.Application.Contracts.Settings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.DeleteFinanceSettings
{
    public class DeleteFinanceSettingsCommandHandler : IRequestHandler<DeleteFinanceSettingsCommand, bool>
    {
        private readonly IFinanceSettingsRepository _repository;

        public DeleteFinanceSettingsCommandHandler(IFinanceSettingsRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteFinanceSettingsCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                throw new Exception("Finance setting not found.");

            await _repository.DeleteAsync(entity);
            return true;
        }
    }
}
