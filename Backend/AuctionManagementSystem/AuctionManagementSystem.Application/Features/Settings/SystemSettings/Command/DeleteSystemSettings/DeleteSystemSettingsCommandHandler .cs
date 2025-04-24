using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Settings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.SystemSettings.Command.DeleteSystemSettings
{
    public class DeleteSystemSettingsCommandHandler : IRequestHandler<DeleteSystemSettingsCommand, bool>
    {
        private readonly ISystemSettingsRepository _repository;

        public DeleteSystemSettingsCommandHandler(ISystemSettingsRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteSystemSettingsCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteSystemSettingsAsync(request.Id);
        }
    }

}
