using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Domain.Entities.Settings;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.CreateFinanceSettings
{
    public class CreateFinanceSettingsCommandHandler : IRequestHandler<CreateFinanceSettingsCommand, FinanceSettingsDto>
    {
        private readonly IFinanceSettingsRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<FinanceSettingsDto> _validator; // Inject the validator

        // Constructor to inject dependencies
        public CreateFinanceSettingsCommandHandler(
            IFinanceSettingsRepository repository,
            IMapper mapper,
            IValidator<FinanceSettingsDto> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        // Handle the request
        public async Task<FinanceSettingsDto> Handle(CreateFinanceSettingsCommand request, CancellationToken cancellationToken)
        {
            // Ensure FinanceSettings is not null
            if (request.FinanceSettings == null)
                throw new ArgumentNullException(nameof(request.FinanceSettings), "FinanceSettings data cannot be null.");

            // Validate the request data using the validator
            var validationResult = await _validator.ValidateAsync(request.FinanceSettings, cancellationToken);

            // If validation fails, throw a ValidationException with the validation errors
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Map the FinanceSettings DTO to the domain entity (TblFinanceSetting)
            var entity = _mapper.Map<TblFinanceSetting>(request.FinanceSettings);

            // Create the entity in the repository and save it
            var createdEntity = await _repository.AddAsync(entity, cancellationToken);

            // Return the created entity as a DTO
            return _mapper.Map<FinanceSettingsDto>(createdEntity);
        }
    }
}
