using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Query.GetStaticPagesSettingsById
{
    public class GetStaticPagesSettingsByIdQueryHandler : IRequestHandler<GetStaticPagesSettingsByIdQuery, StaticPagesSettingsDto>
    {
        private readonly IStaticPagesSettingsRepository _repository;
        private readonly IMapper _mapper;

        public GetStaticPagesSettingsByIdQueryHandler(IStaticPagesSettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<StaticPagesSettingsDto> Handle(GetStaticPagesSettingsByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new Exception("Static Page Setting not found.");
            }

            return _mapper.Map<StaticPagesSettingsDto>(entity);
        }
    }
}
