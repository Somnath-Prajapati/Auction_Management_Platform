using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Domain.Entities.Settings;
using AutoMapper;
using MediatR;
using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Application.Interfaces.Repositories;

namespace AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Command.UpdateStaticPagesSettings
{
    public class UpdateStaticPagesSettingsCommandHandler : IRequestHandler<UpdateStaticPagesSettingsCommand, StaticPagesSettingsDto>
    {
        private readonly IStaticPagesSettingsRepository _repository;
        private readonly IMapper _mapper;

        public UpdateStaticPagesSettingsCommandHandler(IStaticPagesSettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<StaticPagesSettingsDto> Handle(UpdateStaticPagesSettingsCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                throw new Exception("Static Page Setting not found");

            // Map from SettingsDto inside the command to the entity
            _mapper.Map(request.SettingsDto, entity);  // Corrected to use SettingsDto from the command

            await _repository.UpdateAsync(entity);
            return _mapper.Map<StaticPagesSettingsDto>(entity);
        }
    }
}
