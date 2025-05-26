using AuctionManagementSystem.Application.Features.AuditTrail.Queries.GetAllAuditTrails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.Api.Controller.AuditTrail
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditTrailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuditTrailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllAuditTrailsQuery());
            return Ok(result);
        }
    }

}
