using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Repositories;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Query.GetFooterLinksSettingById
{
    public class GetFooterLinksSettingByIdQueryHandler : IRequestHandler<GetFooterLinksSettingByIdQuery, FooterLinksSettingsDto>
    {
        private readonly IFooterLinksSettingsRepository _repository;
        private readonly IMapper _mapper;

        public GetFooterLinksSettingByIdQueryHandler(IFooterLinksSettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FooterLinksSettingsDto> Handle(GetFooterLinksSettingByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<FooterLinksSettingsDto>(entity);
        }
    }
}
