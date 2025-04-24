using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Features.Settings.DirectSaleSettings.Commands.DeleteDirectSaleSettings
{
    public class DeleteDirectSaleSettingCommandHandler : IRequestHandler<DeleteDirectSaleSettingCommand, bool>
    {
        private readonly IDirectSaleSettingsRepository _repository;

        public DeleteDirectSaleSettingCommandHandler(IDirectSaleSettingsRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteDirectSaleSettingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                return false; // or throw NotFoundException
            }

            await _repository.DeleteAsync(entity, cancellationToken);
            return true;
        }
    }
}
