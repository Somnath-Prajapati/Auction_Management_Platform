using AuctionManagementSystem.Application.Dtos.Roles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AuctionManagementSystem.Application.Features.Roles.Query.GetAllRoles;

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
    }
}
