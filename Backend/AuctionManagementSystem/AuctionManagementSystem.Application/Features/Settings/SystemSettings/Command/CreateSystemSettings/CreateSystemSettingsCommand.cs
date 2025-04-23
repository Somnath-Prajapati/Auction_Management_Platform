using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Settings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.SystemSettings.Command.CreateSystemSettings
{
    public class CreateSystemSettingsCommand : IRequest<SystemSettingsDto>
    {
        public SystemSettingsDto SystemSettingsDto { get; set; }

        public CreateSystemSettingsCommand(SystemSettingsDto dto)
        {
            SystemSettingsDto = dto;
        }
    }

}
