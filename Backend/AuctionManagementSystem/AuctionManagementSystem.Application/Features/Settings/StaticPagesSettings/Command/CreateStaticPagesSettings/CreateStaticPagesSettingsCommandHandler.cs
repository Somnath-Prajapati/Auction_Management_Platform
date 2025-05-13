using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Command.CreateStaticPagesSettings;
using AuctionManagementSystem.Application.Interfaces.Repositories;
using AuctionManagementSystem.Domain.Entities.Settings;
using AutoMapper;
using MediatR;

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
        try
        {
            // Map from the DTO inside the command
            var entity = _mapper.Map<TblStaticPagesSettingDto>(request.SettingsDto);

            // Add the entity to the repository
            await _repository.AddAsync(entity);

            // Return the mapped DTO after adding the entity
            return _mapper.Map<StaticPagesSettingsDto>(entity);
        }
        catch (Exception ex)
        {
            // Log or handle the error as needed
            throw new ApplicationException("An error occurred while creating the Static Pages Settings.", ex);
        }
    }
}
