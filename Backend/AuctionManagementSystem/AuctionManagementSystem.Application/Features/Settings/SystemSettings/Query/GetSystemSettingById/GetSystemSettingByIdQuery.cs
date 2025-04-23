using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Settings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.SystemSettings.Query.GetSystemSettingById
{
    public class GetSystemSettingByIdQuery : IRequest<SystemSettingsDto>
    {
        public int Id { get; set; }

        public GetSystemSettingByIdQuery(int id)
        {
            Id = id;
        }
    }
}
