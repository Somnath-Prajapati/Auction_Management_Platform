using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Features.Settings.SystemSettings.Query.GetSystemSettings;
using AutoMapper;
using MediatR;

public class GetSystemSettingsQueryHandler : IRequestHandler<GetSystemSettingsQuery, SystemSettingsDto>
{
    private readonly ISystemSettingsRepository _repository;
    private readonly IMapper _mapper;

    public GetSystemSettingsQueryHandler(ISystemSettingsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<SystemSettingsDto> Handle(GetSystemSettingsQuery request, CancellationToken cancellationToken)
    {
        // Fetch system settings from repository
        var settings = await _repository.GetSystemSettingsAsync();

        // Map the retrieved entity to DTO and return
        return _mapper.Map<SystemSettingsDto>(settings);
    }
}
