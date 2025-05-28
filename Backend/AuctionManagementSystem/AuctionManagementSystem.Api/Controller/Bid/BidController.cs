using AuctionManagementSystem.Application.Dtos.Bids;
using AuctionManagementSystem.Application.Features.Bids.AutoBid.Command.CreateAutoBid;
using AuctionManagementSystem.Application.Features.Bids.AutoBid.Command.DeleteAutoBid;
using AuctionManagementSystem.Application.Features.Bids.AutoBid.Query.GetById;
using AuctionManagementSystem.Application.Features.Bids.CreateBid.Command;
using AuctionManagementSystem.Application.Features.Bids.CreateBid.Query.GetBidById;
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

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveAutoBid(AutoBidRemoveCommand command)
        {

            if (command == null)
                return NotFound("AutoBid not found for given auction, asset, and user.");

            await _mediator.Send(command);
            return Ok("AutoBid removed successfully.");
        }


        [HttpGet("GetAutoData/{userId}/{auctionId}/{assetId}")]
        public async Task<IActionResult> GetAutoBidById(int userId, int auctionId, int assetId)
        {
            // Use parameters directly without a query DTO
            var result = await _mediator.Send(new GetAutoBidByIdQuery(userId , auctionId , assetId));

            if (result == null)
                return NotFound();

            return Ok(result);
        }


    }
}
