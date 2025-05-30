using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Application.Dtos.UserDtos;
using AuctionManagementSystem.Application.Features.Notifications.Command.MarkAsRead;
using AuctionManagementSystem.Application.Features.UserFeature.Command.CreateUser;
using AuctionManagementSystem.Application.Features.UserFeature.Command.DeleteNotificationByUserId;
using AuctionManagementSystem.Application.Features.UserFeature.Command.DeleteUser;
using AuctionManagementSystem.Application.Features.UserFeature.Command.UpdateUser;
using AuctionManagementSystem.Application.Features.UserFeature.Query.GetAllUser;
using AuctionManagementSystem.Application.Features.UserFeature.Query.GetNotificationByUserId;
using AuctionManagementSystem.Application.Features.UserFeature.Query.GetRoles;
using AuctionManagementSystem.Application.Features.UserFeature.Query.GetStatus;
using AuctionManagementSystem.Application.Features.UserFeature.Query.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.Api.Controller.User
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggedInUserService _loggedInUserService;

        public UserController(IMediator mediator, ILoggedInUserService loggedInUserService)
        {
            _mediator = mediator;
            _loggedInUserService = loggedInUserService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AllUser()
        {
            var Query = new GetAllUsersQuery();
            var Users = await _mediator.Send(Query);
            return Ok(Users);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("Add")]

        public async Task<IActionResult> CreateUser([FromForm] UserDto dto)
        {
            var userid = _loggedInUserService.UserId;
            var command = new CreateUserCommand(dto, userid);
            var userId = await _mediator.Send(command);
            return Ok(new { UserId = userId });
        }

        [AllowAnonymous]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromForm] UserDto dto)
        {
            var userid = _loggedInUserService.UserId;
            var result = await _mediator.Send(new UpdateUserCommand(id, dto,userid));
            return Ok(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userid = _loggedInUserService.UserId;
            var result = await _mediator.Send(new DeleteUserCommand(id,userid));
            return result ? Ok() : NotFound();
        }
        [AllowAnonymous]
        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _mediator.Send(new GetAllRolesQuery());
            return Ok(roles);
        }
        [AllowAnonymous]
        [HttpGet("statuses")]
        public async Task<IActionResult> GetAllStatuses()
        {
            var statuses = await _mediator.Send(new GetAllStatusesQuery());
            return Ok(statuses);
        }
        [AllowAnonymous]
        [HttpGet("Notification/{UserId}")]
        public async Task<IActionResult> GetUserNotifications(int UserId)
        {
            var Query = new GetNotificationByUserIdQuery(UserId);
            var result = await _mediator.Send(Query);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpDelete("delete-all-notification/{userId}")]
        public async Task<IActionResult> DeleteAllNotifications(int userId)
        {
            await _mediator.Send(new DeleteNotificationByUserIdCommand(userId));
            return NoContent();
        }
        //notification Handler
        [AllowAnonymous]
        [HttpPut("mark-as-read/{notificationId}")]
        public async Task<IActionResult> MarkAsRead(Guid notificationId)
        {
            await _mediator.Send(new MarkAsReadCommand(notificationId));
            return NoContent();
        }
    }
}
