using AuctionManagementSystem.Application.Dtos.UserDtos;
using AuctionManagementSystem.Application.Features.UserFeature.Command.CreateUser;
using AuctionManagementSystem.Application.Features.UserFeature.Command.DeleteUser;
using AuctionManagementSystem.Application.Features.UserFeature.Command.UpdateUser;
using AuctionManagementSystem.Application.Features.UserFeature.Query.GetAllUser;
using AuctionManagementSystem.Application.Features.UserFeature.Query.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
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
        public async Task<IActionResult> CreateUser([FromBody] UserDto dto)
        {
            var command = new CreateUserCommand(dto);
            var userId = await _mediator.Send(command);
            return Ok(new { UserId = userId });
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto dto)
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

    }
}
