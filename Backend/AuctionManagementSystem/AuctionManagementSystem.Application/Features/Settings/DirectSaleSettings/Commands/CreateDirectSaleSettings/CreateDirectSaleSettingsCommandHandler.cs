using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Domain.Entities.Settings;
using AuctionManagementSystem.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.DirectSaleSettings.Commands.CreateDirectSaleSettings
{
    public class CreateDirectSaleSettingsCommandHandler : IRequestHandler<CreateDirectSaleSettingsCommand, DirectSaleSettingsDto>
    {
        private readonly IDirectSaleSettingsRepository _repository;
        private readonly IMapper _mapper;

        public CreateDirectSaleSettingsCommandHandler(IDirectSaleSettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DirectSaleSettingsDto> Handle(CreateDirectSaleSettingsCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TblDirectSaleSetting>(request);
            await _repository.AddAsync(entity);
            return _mapper.Map<DirectSaleSettingsDto>(entity);
        }
    }
}
