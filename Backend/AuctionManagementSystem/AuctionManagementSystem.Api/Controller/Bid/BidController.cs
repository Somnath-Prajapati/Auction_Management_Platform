using AuctionManagementSystem.Application.Features.Bids.Command.CreateBid;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.Api.Controller.Bid
{
      [ApiController]
      [Route("api/[controller]")]
      public class BidController : ControllerBase
      {
          private readonly IMediator _mediator;

          public BidController(IMediator mediator)
          {
                _mediator = mediator;
          }

          [HttpPost("place")]
          public async Task<IActionResult> PlaceBid([FromBody] AddBidCommand command, CancellationToken cancellationToken)
          {
              var bidId = await _mediator.Send(command, cancellationToken);
              return Ok(new { BidId = bidId });
          }
      }
}
