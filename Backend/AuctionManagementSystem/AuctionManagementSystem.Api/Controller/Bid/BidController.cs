using AuctionManagementSystem.Application.Features.Bids.Command.CreateBid;
using AuctionManagementSystem.Application.Features.Bids.Query.GetBidById;
using AuctionManagementSystem.Application.Features.Bids.Query.GetBidByUserId;
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
         public async Task<IActionResult> PlaceBid([FromBody] AddBidCommand command)
         {
              var bidId = await _mediator.Send(command);
              return Ok(new { BidId = bidId });
         }
        [HttpGet("Assetstats/{assetId}")]
        public async Task<IActionResult> GetBidStats(int assetId)
        {
            var query = new GetBidStatsByAssetIdQuery(assetId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("UserBids/{userId}")]
        public async Task<IActionResult> GetBidsByUserId(int userId)
        {
            var query = new GetBidByUserIdQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
      }
}
