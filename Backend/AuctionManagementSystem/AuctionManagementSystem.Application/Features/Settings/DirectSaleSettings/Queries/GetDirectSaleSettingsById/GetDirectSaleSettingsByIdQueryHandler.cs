using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.DirectSaleSettings.Queries.GetDirectSaleSettingsById
{
    public class GetDirectSaleSettingsByIdQueryHandler : IRequestHandler<GetDirectSaleSettingByIdQuery, DirectSaleSettingsDto>
    {
        private readonly IDirectSaleSettingsRepository _repository;
        private readonly IMapper _mapper;

        public GetDirectSaleSettingsByIdQueryHandler(IDirectSaleSettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DirectSaleSettingsDto> Handle(GetDirectSaleSettingByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                throw new Exception("Direct Sale setting not found.");
            return _mapper.Map<DirectSaleSettingsDto>(entity);
        }
    }
}
