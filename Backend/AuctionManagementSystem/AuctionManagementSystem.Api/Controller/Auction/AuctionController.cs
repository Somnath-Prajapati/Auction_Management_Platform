using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Application.Dtos.Auctions;
using AuctionManagementSystem.Application.Features.Auctions.Commands.CreateAuction;
using AuctionManagementSystem.Application.Features.Auctions.Commands.DeleteAuction;
using AuctionManagementSystem.Application.Features.Auctions.Commands.UpdateAuction;
using AuctionManagementSystem.Application.Features.Auctions.Queries.GetAllAuctions;
using AuctionManagementSystem.Application.Features.Auctions.Queries.GetAuctionById;
using AuctionManagementSystem.Application.Features.Auctions.Queries.GetAuctionsByIds;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.Api.Controller.Auction
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuctionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggedInUserService _loggedInUserService;


        public AuctionController(IMediator mediator, ILoggedInUserService loggedInUserService)
        {
            _mediator = mediator;
            _loggedInUserService = loggedInUserService;

        }

        // GET: api/auction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuctionDto>>> GetAllAuctions()
        {
            var auctions = await _mediator.Send(new GetAllAuctionsQuery());
            return Ok(auctions);
        }

        // GET: api/auction/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionDto>> GetAuctionById(int id)
        {
            var auction = await _mediator.Send(new GetAuctionByIdQuery { AuctionId = id });
            if (auction == null)
                return NotFound();
            return Ok(auction);
        }

        // POST: api/auction
        [HttpPost]
        public async Task<ActionResult<AuctionDto>> CreateAuction([FromBody] CreateAuctionCommand command)
        {
            var userId = _loggedInUserService.UserId;
            command.UserId = userId;

            if (command == null)
            {
                return BadRequest("Invalid request");
            }

            var createdAuction = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetAuctionById), new { id = createdAuction.AuctionId }, createdAuction);
        }


        // PUT: api/auction/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuction(int id, [FromBody] UpdateAuctionCommand command)
        {
            if (id != command.AuctionId)
                return BadRequest("ID mismatch");

            var result = await _mediator.Send(command);
            if (!result)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/auction/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteAuctionCommand { AuctionId = id };
            var result = await _mediator.Send(command);
            return result ? Ok() : NotFound();
        }
        [HttpGet("get-by-ids")]
        public async Task<IActionResult> GetAuctionsByIds([FromQuery] string auctionIds)
        {
            var ids = auctionIds.Split(',').Select(int.Parse).ToList();
            var result = await _mediator.Send(new GetAuctionsByIdsQuery(ids));
            return Ok(result);
        }

    }
}
