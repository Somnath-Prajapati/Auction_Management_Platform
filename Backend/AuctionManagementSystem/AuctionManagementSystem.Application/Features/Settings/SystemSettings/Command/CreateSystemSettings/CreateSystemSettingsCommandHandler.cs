using AutoMapper;
using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Interfaces.Repositories;
using AuctionManagementSystem.Domain.Entities.Settings;
using MediatR;
using AuctionManagementSystem.Application.Contracts.Settings;

namespace AuctionManagementSystem.Application.Features.Settings.SystemSettings.Command.CreateSystemSettings
{
    public class CreateSystemSettingsCommandHandler : IRequestHandler<CreateSystemSettingsCommand, SystemSettingsDto>
    {
        private readonly ISystemSettingsRepository _repository;
        private readonly IMapper _mapper;

        public CreateSystemSettingsCommandHandler(ISystemSettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SystemSettingsDto> Handle(CreateSystemSettingsCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TblSystemSetting>(request.SystemSettingsDto);
            entity.CreatedAt = DateTime.UtcNow;

            await _repository.CreateSystemSettingsAsync(entity);

            return _mapper.Map<SystemSettingsDto>(entity);
        }
    }
}
