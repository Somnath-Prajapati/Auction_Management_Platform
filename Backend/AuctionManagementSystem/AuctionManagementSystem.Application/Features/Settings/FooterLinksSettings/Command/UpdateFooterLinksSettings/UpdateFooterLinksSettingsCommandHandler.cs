using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Repositories;
using AuctionManagementSystem.Domain.Entities.Settings;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Command.UpdateFooterLinksSettings
{
    public class UpdateFooterLinksSettingsCommandHandler : IRequestHandler<UpdateFooterLinksSettingsCommand, FooterLinksSettingsDto>
    {
        private readonly IFooterLinksSettingsRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<FooterLinksSettingsDto> _validator;

        public UpdateFooterLinksSettingsCommandHandler(
            IFooterLinksSettingsRepository repository,
            IMapper mapper,
            IValidator<FooterLinksSettingsDto> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<FooterLinksSettingsDto> Handle(UpdateFooterLinksSettingsCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.FooterLinksSettings, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existing = await _repository.GetByIdAsync(request.FooterLinksSettings.Id);
            if (existing == null)
                throw new KeyNotFoundException("Footer links setting not found.");

            _mapper.Map(request.FooterLinksSettings, existing);
            var updated = await _repository.UpdateAsync(existing);
            return _mapper.Map<FooterLinksSettingsDto>(updated);
        }
    }
}
