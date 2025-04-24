using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Query.GetAllStaticPagesSettings
{
    public class GetAllStaticPagesSettingsQueryHandler : IRequestHandler<GetAllStaticPagesSettingsQuery, List<StaticPagesSettingsDto>>
    {
        private readonly IStaticPagesSettingsRepository _repository;
        private readonly IMapper _mapper;

        public GetAllStaticPagesSettingsQueryHandler(IStaticPagesSettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<StaticPagesSettingsDto>> Handle(GetAllStaticPagesSettingsQuery request, CancellationToken cancellationToken)
        {
            var settings = await _repository.GetAllAsync();
            return _mapper.Map<List<StaticPagesSettingsDto>>(settings);
        }
    }
}
