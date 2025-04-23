using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Domain.Entities.Settings;
using AutoMapper;
using FluentValidation;
using MediatR;
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
        public CreateFinanceSettingsCommandHandler(IFinanceSettingsRepository repository, IMapper mapper, IValidator<FinanceSettingsDto> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator; // Initialize the validator
        }

        // Handle the request
        public async Task<FinanceSettingsDto> Handle(CreateFinanceSettingsCommand request, CancellationToken cancellationToken)
        {
            // Validate the request data using the validator
            var validationResult = await _validator.ValidateAsync(request.FinanceSettings, cancellationToken);

            // If validation fails, throw a ValidationException with the validation errors
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Proceed with the creation logic if validation is successful
            var entity = _mapper.Map<TblFinanceSetting>(request);
            var createdEntity = await _repository.AddAsync(entity, cancellationToken);
            return _mapper.Map<FinanceSettingsDto>(createdEntity);
        }
    }
}
