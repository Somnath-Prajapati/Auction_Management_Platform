using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Application.Dtos.Settings;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.SystemSettings.Query.GetSystemSettingById
{
    public class GetSystemSettingByIdQueryHandler : IRequestHandler<GetSystemSettingByIdQuery, SystemSettingsDto>
    {
        private readonly ISystemSettingsRepository _systemSettingRepository;
        private readonly IMapper _mapper;

        public GetSystemSettingByIdQueryHandler(ISystemSettingsRepository systemSettingRepository, IMapper mapper)
        {
            _systemSettingRepository = systemSettingRepository;
            _mapper = mapper;
        }

        public async Task<SystemSettingsDto> Handle(GetSystemSettingByIdQuery request, CancellationToken cancellationToken)
        {
            var systemSetting = await _systemSettingRepository.GetByIdAsync(request.Id);

            if (systemSetting == null)
            {
                // Handle the case when the setting is not found
                return null;
            }

            return _mapper.Map<SystemSettingsDto>(systemSetting);
        }
    }

}
