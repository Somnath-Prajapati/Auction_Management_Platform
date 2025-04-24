using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Repositories;
using AuctionManagementSystem.Domain.Entities.Settings;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Command.CreateFooterLinksSettings
{
    public class CreateFooterLinksSettingsCommandHandler : IRequestHandler<CreateFooterLinksSettingsCommand, FooterLinksSettingsDto>
    {
        private readonly IFooterLinksSettingsRepository _repository;
        private readonly IMapper _mapper;

        public CreateFooterLinksSettingsCommandHandler(IFooterLinksSettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FooterLinksSettingsDto> Handle(CreateFooterLinksSettingsCommand request, CancellationToken cancellationToken)
        {
            // Mapping from FooterLinksSettingsDto to TblFooterLinksSetting
            var entity = _mapper.Map<TblFooterLinksSetting>(request.FooterLinksSettingsDto);

            // Creating the entity in the repository
            var created = await _repository.CreateAsync(entity);

            // Returning the created entity as a DTO
            return _mapper.Map<FooterLinksSettingsDto>(created);
        }
    }
}
