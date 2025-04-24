using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Features.Settings.DirectSaleSettings.Queries.GetDirectSaleSettingsById;
using AuctionManagementSystem.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Query.GetStaticPagesSettingsById
{
    public class GetDirectSaleSettingByIdQueryHandler : IRequestHandler<GetDirectSaleSettingByIdQuery, DirectSaleSettingsDto>
    {
        private readonly IDirectSaleSettingsRepository _repository;
        private readonly IMapper _mapper;

        public GetDirectSaleSettingByIdQueryHandler(IDirectSaleSettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DirectSaleSettingsDto> Handle(GetDirectSaleSettingByIdQuery request, CancellationToken cancellationToken)
        {
            var setting = await _repository.GetByIdAsync(request.Id);

            if (setting == null)
                throw new KeyNotFoundException($"Direct Sale Setting with ID {request.Id} not found.");

            return _mapper.Map<DirectSaleSettingsDto>(setting);
        }
    }
}
