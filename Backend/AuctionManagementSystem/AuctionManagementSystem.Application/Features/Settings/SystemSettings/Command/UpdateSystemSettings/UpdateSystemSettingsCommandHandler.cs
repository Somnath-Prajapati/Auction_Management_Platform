using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Application.Dtos.Settings;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Features.Settings.SystemSettings.Command.UpdateSystemSettings
{
    public class UpdateSystemSettingsCommandHandler : IRequestHandler<UpdateSystemSettingsCommand, SystemSettingsDto>
    {
        private readonly ISystemSettingsRepository _repository;
        private readonly IMapper _mapper;

        public UpdateSystemSettingsCommandHandler(ISystemSettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SystemSettingsDto> Handle(UpdateSystemSettingsCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.SettingsDto.Id);
            if (entity == null) return null;

            _mapper.Map(request.SettingsDto, entity);
            entity.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateSystemSettingsAsync(entity);

            return _mapper.Map<SystemSettingsDto>(entity);
        }
    }
}
