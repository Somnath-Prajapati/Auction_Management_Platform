using AuctionManagementSystem.Application.Features.AuthFeatures.Command.VerifyOtp;
using AuctionManagementSystem.Application.Features.AuthFeatures.Command;
using AuctionManagementSystem.Application.Dtos.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AuctionManagementSystem.Application.Features.UserFeature.Query.GetAllUser;

namespace AuctionManagementSystem.Api.Controller.Auth
{

        [ApiController]
        [Route("api/[controller]")]
        public class AuthController : ControllerBase
        {
            private readonly IMediator _mediator;

            public AuthController(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpPost("generate-otp")]
            public async Task<IActionResult> GenerateOtp([FromBody] GenerateOtpCommand command)
            {
                var result = await _mediator.Send(command);
                return result ? Ok(new { message = "OTP sent." }) : BadRequest( new { message = "User not found." });
            }

            [HttpPost("verify-otp")]
            public async Task<IActionResult> VerifyOtp([FromBody] AuthRequestDto dto)
            {
                var Query = new VerifyOtpCommand(dto);
                var Result = await _mediator.Send(Query);
           
                  if(Result == null)
                    {
                        return Unauthorized(new { message = "Invalid OTP" });
                    }
            return Ok(Result);
            }
        }
    }
