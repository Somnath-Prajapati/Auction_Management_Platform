using AuctionManagementSystem.Application.Dtos.Bids;
using AuctionManagementSystem.Application.Features.Bids.AutoBid.Command.CreateAutoBid;
using AuctionManagementSystem.Application.Features.Bids.Command.CreateBid;
using AuctionManagementSystem.Application.Features.Bids.Query.GetBidById;
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




        [HttpPost("auto")]
        public async Task<IActionResult> PlaceAutoBid([FromBody] AddAutoBidCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var bidId = await _mediator.Send(command);
                return Ok(new { BidId = bidId, Message = "Auto-bid placed successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }



    }
}
