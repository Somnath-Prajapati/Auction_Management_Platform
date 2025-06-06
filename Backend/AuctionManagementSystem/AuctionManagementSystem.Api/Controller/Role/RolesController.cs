using AuctionManagementSystem.Application.Dtos.Roles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AuctionManagementSystem.Application.Features.Roles.Query.GetAllRoles;
using AuctionManagementSystem.Application.Features.Roles.Query.GetPermissionsByRoleId;
using AuctionManagementSystem.Application.Features.Roles.Command.CreateRoles;
using AuctionManagementSystem.Application.Features.Roles.Command.UpdateRoles;
using AuctionManagementSystem.Application.Features.Roles.Command.DeleteRoles;

namespace AuctionManagementSystem.Api.Controller.Role
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleWithPermissionsDto>>> GetAllRolesWithPermissions()
        {
            var roles = await _mediator.Send(new GetAllRolesWithPermissionsQuery());
            return Ok(roles);
        }

        [HttpGet("{roleId}")]
        public async Task<ActionResult<RoleWithPermissionsDto>> GetRolePermissionsById(int roleId)
        {
            var role = await _mediator.Send(new GetPermissionsByRoleIdQuery(roleId));
            if (role == null)
                return NotFound();

            return Ok(role);
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateRoleWithPermissions([FromBody] RoleWithPermissionsDto roleDto)
        {
            var roleId = await _mediator.Send(new CreateRoleWithPermissionsCommand(roleDto));
            return CreatedAtAction(nameof(GetRolePermissionsById), new { roleId = roleId }, roleId);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoleWithPermissions(int id, [FromBody] RoleWithPermissionsDto roleDto)
        {
            if (id != roleDto.RoleId) return BadRequest();

            var command = new UpdateRoleWithPermissionsCommand(roleDto);
            var result = await _mediator.Send(command);

            if (!result) return NotFound();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var command = new DeleteRoleCommand(id);
            var result = await _mediator.Send(command);

            if (!result) return NotFound();

            return NoContent();
        }



    }
}
