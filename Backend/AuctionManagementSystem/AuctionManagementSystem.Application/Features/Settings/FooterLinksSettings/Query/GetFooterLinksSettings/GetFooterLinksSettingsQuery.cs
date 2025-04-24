using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Settings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Query.GetFooterLinksSettings
{
    public class GetFooterLinksSettingsQuery : IRequest<FooterLinksSettingsDto> { }

}
