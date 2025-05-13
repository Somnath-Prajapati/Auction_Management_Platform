using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Domain.Entities.Settings;
using AutoMapper;
using MediatR;
using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Application.Interfaces.Repositories;
using AuctionManagementSystem.Application.Exceptions;
using FluentValidation;

namespace AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Command.UpdateStaticPagesSettings
{
    public class UpdateStaticPagesSettingsCommandHandler : IRequestHandler<UpdateStaticPagesSettingsCommand, StaticPagesSettingsDto>
    {
        private readonly IStaticPagesSettingsRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateStaticPagesSettingsCommand> _validator;

        public UpdateStaticPagesSettingsCommandHandler(IStaticPagesSettingsRepository repository, IMapper mapper, IValidator<UpdateStaticPagesSettingsCommand> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<StaticPagesSettingsDto> Handle(UpdateStaticPagesSettingsCommand request, CancellationToken cancellationToken)
        {
            // Validate the command itself (including SettingsDto)
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Get the entity by Id
            var entity = await _repository.GetByIdAsync(request.SettingsDto.Id);
            if (entity == null)
            {
                throw new NotFoundException("Static Page Setting not found.");
            }

            // Map the DTO to the entity
            _mapper.Map(request.SettingsDto, entity);

            // Update the entity
            await _repository.UpdateAsync(entity);

            // Return the updated entity as DTO
            return _mapper.Map<StaticPagesSettingsDto>(entity);
        }
    }
}
