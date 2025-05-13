using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Repositories;
using AuctionManagementSystem.Domain.Entities.Settings;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Command.CreateFooterLinksSettings
{
    public class CreateFooterLinksSettingsCommandHandler : IRequestHandler<CreateFooterLinksSettingsCommand, FooterLinksSettingsDto>
    {
        private readonly IFooterLinksSettingsRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<FooterLinksSettingsDto> _validator;

        public CreateFooterLinksSettingsCommandHandler(
            IFooterLinksSettingsRepository repository,
            IMapper mapper,
            IValidator<FooterLinksSettingsDto> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<FooterLinksSettingsDto> Handle(CreateFooterLinksSettingsCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.FooterLinksSettingsDto, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var entity = _mapper.Map<TblFooterLinksSetting>(request.FooterLinksSettingsDto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<FooterLinksSettingsDto>(created);
        }
    }
}
