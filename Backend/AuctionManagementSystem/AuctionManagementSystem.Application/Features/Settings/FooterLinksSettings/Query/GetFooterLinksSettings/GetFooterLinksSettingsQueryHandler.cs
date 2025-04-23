using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Repositories;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Query.GetFooterLinksSettings
{
    public class GetFooterLinksSettingsQueryHandler : IRequestHandler<GetFooterLinksSettingsQuery, FooterLinksSettingsDto>
    {
        private readonly IFooterLinksSettingsRepository _repository;
        private readonly IMapper _mapper;

        public GetFooterLinksSettingsQueryHandler(IFooterLinksSettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FooterLinksSettingsDto> Handle(GetFooterLinksSettingsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAsync();
            return _mapper.Map<FooterLinksSettingsDto>(entity);
        }
    }
}
