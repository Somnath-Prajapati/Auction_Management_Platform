using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AuctionManagementSystem.Application.Features.AuthFeatures.Command
{
    public record GenerateOtpCommand(string Email) : IRequest<bool>;
}
