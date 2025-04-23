using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Domain.Entities.Settings;
using AutoMapper;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.UpdateFinanceSettings
{
    public class UpdateFinanceSettingsCommandHandler : IRequestHandler<UpdateFinanceSettingsCommand, FinanceSettingsDto>
    {
        private readonly IFinanceSettingsRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<FinanceSettingsDto> _validator; // Inject the validator

        // Constructor
        public UpdateFinanceSettingsCommandHandler(IFinanceSettingsRepository repository, IMapper mapper, IValidator<FinanceSettingsDto> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator; // Initialize the validator
        }

        // Handle the command
        public async Task<FinanceSettingsDto> Handle(UpdateFinanceSettingsCommand request, CancellationToken cancellationToken)
        {
            // Validate the FinanceSettings property of the request
            var validationResult = await _validator.ValidateAsync(request.FinanceSettings, cancellationToken);

            // If validation fails, throw an exception with the validation errors
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Proceed with the business logic if validation is successful
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new Exception("Finance setting not found.");
            }

            _mapper.Map(request.FinanceSettings, entity);

            await _repository.UpdateAsync(entity);

            return _mapper.Map<FinanceSettingsDto>(entity);
        }
    }
}
