using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Application.Dtos.Settings;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Query.GetAllFinanceSettings
{
    public class GetAllFinanceSettingsQueryHandler : IRequestHandler<GetAllFinanceSettingsQuery, List<FinanceSettingsDto>>
    {
        private readonly IFinanceSettingsRepository _repository;
        private readonly IMapper _mapper;

        public GetAllFinanceSettingsQueryHandler(IFinanceSettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FinanceSettingsDto>> Handle(GetAllFinanceSettingsQuery request, CancellationToken cancellationToken)
        {
            var settings = await _repository.GetAllAsync();
            return _mapper.Map<List<FinanceSettingsDto>>(settings);
        }
    }
}
