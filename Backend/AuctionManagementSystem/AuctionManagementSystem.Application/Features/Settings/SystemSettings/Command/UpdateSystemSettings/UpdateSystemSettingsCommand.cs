using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Settings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.SystemSettings.Command.UpdateSystemSettings
{
    public class UpdateSystemSettingsCommand : IRequest<SystemSettingsDto>
    {
        public SystemSettingsDto SettingsDto { get; }

        public UpdateSystemSettingsCommand(SystemSettingsDto settingsDto)
        {
            SettingsDto = settingsDto;
        }
    }
}
