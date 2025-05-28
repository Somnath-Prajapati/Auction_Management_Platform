using AuctionManagementSystem.Application.Features.AuthFeatures.Command.VerifyOtp;
using AuctionManagementSystem.Application.Features.AuthFeatures.Command;
using AuctionManagementSystem.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets.Auth;

namespace AuctionManagementSystem.Api.Controller.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger; 

        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("generate-otp")]
        public async Task<IActionResult> GenerateOtp([FromBody] GenerateOtpCommand command)
        {
            var result = await _mediator.Send(command);
            return result ? Ok(new { message = "OTP sent." }) : BadRequest(new { message = "User not found." });
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] AuthRequestDto dto)
        {
            var Query = new VerifyOtpCommand(dto);
            var Result = await _mediator.Send(Query);

            if (Result == null)
            {
                return Unauthorized(new { message = "Invalid OTP" });
            }
            return Ok(Result);
        }

       

        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Starting user registration for email: {Email}", command.Email);
        //        // Validate input
        //        if (string.IsNullOrWhiteSpace(command.Email) || string.IsNullOrWhiteSpace(command.MobileNumber))
        //        {
        //            _logger.LogWarning("Registration failed: Email or mobile number is empty");
        //            return BadRequest(new { message = "Email and mobile number are required." });
        //        }

        //        // Add default role if not provided
        //        if (string.IsNullOrWhiteSpace(command.Role))
        //        {
        //            command.Role = "User"; // Set default role
        //        }

        //        var result = await _mediator.Send(command);
        //        if (result == null)
        //        {
        //            _logger.LogWarning("Registration failed: Result was null");
        //            return BadRequest(new { message = "Registration failed." });
        //        }
        //        _logger.LogInformation("User registered successfully: {UserId}", result.UserId);
        //        return Ok(result);
        //    }
        //    catch (BadRequestException ex)
        //    {
        //        _logger.LogWarning(ex, "Registration failed with BadRequestException");
        //        return BadRequest(new { message = ex.Message });
        //    }
        //    catch (DatabaseException ex)
        //    {
        //        _logger.LogError(ex, "Registration failed with DatabaseException: {Message}", ex.Message);
        //        return BadRequest(new { message = "User registration failed: " + ex.Message.Split('-')[0].Trim() });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Unexpected error during registration");
        //        return StatusCode(500, new { message = "An unexpected error occurred during registration." });
        //    }
        //}
    }
}
    
