using AuctionManagementSystem.Application.Dtos.Settings;
using MediatR;
using System.Collections.Generic;

namespace AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Query.GetAllStaticPagesSettings
{
    public class GetAllStaticPagesSettingsQuery : IRequest<List<StaticPagesSettingsDto>>
    {
    }
}
