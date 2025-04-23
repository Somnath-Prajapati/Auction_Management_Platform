using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Application.Dtos.Settings;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Query.GetFinanceSettingsById
{
    public class GetFinanceSettingsByIdQueryHandler : IRequestHandler<GetFinanceSettingsByIdQuery, FinanceSettingsDto>
    {
        private readonly IFinanceSettingsRepository _repository;
        private readonly IMapper _mapper;

        public GetFinanceSettingsByIdQueryHandler(IFinanceSettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FinanceSettingsDto> Handle(GetFinanceSettingsByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                throw new Exception("Finance setting not found.");

            return _mapper.Map<FinanceSettingsDto>(entity);
        }
    }
}
