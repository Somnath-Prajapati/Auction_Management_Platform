using AuctionManagementSystem.Application.Dtos.UserDtos;
using AuctionManagementSystem.Application.Features.UserFeature.Command.CreateUser;
using AuctionManagementSystem.Application.Features.UserFeature.Command.DeleteUser;
using AuctionManagementSystem.Application.Features.UserFeature.Command.UpdateUser;
using AuctionManagementSystem.Application.Features.UserFeature.Query.GetAllUser;
using AuctionManagementSystem.Application.Features.UserFeature.Query.GetRoles;
using AuctionManagementSystem.Application.Features.UserFeature.Query.GetStatus;
using AuctionManagementSystem.Application.Features.UserFeature.Query.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.Api.Controller.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> AllUser()
        {
            var Query = new GetAllUsersQuery();
            var Users = await _mediator.Send(Query);
            return Ok(Users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> CreateUser([FromForm] UserDto dto)
        {
            var command = new CreateUserCommand(dto);
            var userId = await _mediator.Send(command);
            return Ok(new { UserId = userId });
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromForm] UserDto dto)
        {
            var result = await _mediator.Send(new UpdateUserCommand(id, dto));
            return Ok(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id));
            return result ? Ok() : NotFound();
        }
        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _mediator.Send(new GetAllRolesQuery());
            return Ok(roles);
        }

        [HttpGet("statuses")]
        public async Task<IActionResult> GetAllStatuses()
        {
            var statuses = await _mediator.Send(new GetAllStatusesQuery());
            return Ok(statuses);
        }

    }
}
