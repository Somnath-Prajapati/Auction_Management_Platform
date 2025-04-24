using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Interfaces.Repositories;
using AuctionManagementSystem.Domain.Entities.Settings;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Command.CreateStaticPagesSettings
{
    public class CreateStaticPagesSettingsCommandHandler : IRequestHandler<CreateStaticPagesSettingsCommand, StaticPagesSettingsDto>
    {
        private readonly IStaticPagesSettingsRepository _repository;
        private readonly IMapper _mapper;

        public CreateStaticPagesSettingsCommandHandler(IStaticPagesSettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<StaticPagesSettingsDto> Handle(CreateStaticPagesSettingsCommand request, CancellationToken cancellationToken)
        {
            // Map from the DTO inside the command
            var entity = _mapper.Map<TblStaticPagesSetting>(request.SettingsDto);

            await _repository.AddAsync(entity);

            return _mapper.Map<StaticPagesSettingsDto>(entity);
        }
    }
}
