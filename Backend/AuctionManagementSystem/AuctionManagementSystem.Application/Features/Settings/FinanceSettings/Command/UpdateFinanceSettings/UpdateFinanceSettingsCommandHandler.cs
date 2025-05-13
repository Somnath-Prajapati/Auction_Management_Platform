using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Domain.Entities.Settings;
using AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.UpdateFinanceSettings;
using MediatR;

public class UpdateFinanceSettingsCommandHandler : IRequestHandler<UpdateFinanceSettingsCommand, FinanceSettingsDto>
{
    private readonly IFinanceSettingsRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<FinanceSettingsDto> _validator;

    public UpdateFinanceSettingsCommandHandler(
        IFinanceSettingsRepository repository,
        IMapper mapper,
        IValidator<FinanceSettingsDto> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<FinanceSettingsDto> Handle(UpdateFinanceSettingsCommand request, CancellationToken cancellationToken)
    {
        // Validate the FinanceSettingsDto from the request
        var validationResult = await _validator.ValidateAsync(request.FinanceSettings, cancellationToken);

        if (!validationResult.IsValid)
        {
            // If validation fails, throw a ValidationException with errors
            throw new ValidationException(validationResult.Errors);
        }

        // Fetch the existing finance settings from the repository by Id
        var entity = await _repository.GetByIdAsync(request.FinanceSettings.Id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Finance setting with ID {request.FinanceSettings.Id} not found.");
        }

        // Map the DTO to the entity
        _mapper.Map(request.FinanceSettings, entity);

        // Update the entity in the repository
        await _repository.UpdateAsync(entity);

        // Return the updated DTO
        return _mapper.Map<FinanceSettingsDto>(entity);
    }
}
