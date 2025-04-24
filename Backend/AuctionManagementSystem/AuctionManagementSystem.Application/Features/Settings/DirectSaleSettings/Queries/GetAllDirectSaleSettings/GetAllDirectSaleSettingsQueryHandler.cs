using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.DirectSaleSettings.Queries.GetAllDirectSaleSettings
{
    public class GetAllDirectSaleSettingsQueryHandler : IRequestHandler<GetAllDirectSaleSettingsQuery, List<DirectSaleSettingsDto>>
    {
        private readonly IDirectSaleSettingsRepository _repository;
        private readonly IMapper _mapper;

        public GetAllDirectSaleSettingsQueryHandler(IDirectSaleSettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<DirectSaleSettingsDto>> Handle(GetAllDirectSaleSettingsQuery request, CancellationToken cancellationToken)
        {
            var settings = await _repository.GetAllAsync();
            return _mapper.Map<List<DirectSaleSettingsDto>>(settings);
        }
    }
}
