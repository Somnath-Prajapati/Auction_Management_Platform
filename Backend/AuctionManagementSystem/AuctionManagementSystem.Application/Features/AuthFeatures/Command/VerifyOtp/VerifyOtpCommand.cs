using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets.Auth;
using AuctionManagementSystem.Application.Models;
using MediatR;

namespace AuctionManagementSystem.Application.Features.AuthFeatures.Command.VerifyOtp
{
    public record VerifyOtpCommand(AuthRequestDto dto) : IRequest<AuthResponseDto>;

}
