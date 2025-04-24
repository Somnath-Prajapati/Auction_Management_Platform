using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.SystemSettings.Command.DeleteSystemSettings
{
    public record DeleteSystemSettingsCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteSystemSettingsCommand(int id)
        {
            Id = id;
        }   
    }
}
