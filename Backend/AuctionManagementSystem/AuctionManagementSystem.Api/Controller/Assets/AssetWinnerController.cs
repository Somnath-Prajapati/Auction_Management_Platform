using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Features.Assets.AssetWinner.Command.MarkWinsAsSeen;
using AuctionManagementSystem.Application.Features.Assets.AssetWinner.Query.GetByUserIdAsync;
using AuctionManagementSystem.Application.Features.Assets.AssetWinner.Query.GetUnseenWinsById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.Api.Controller.Assets
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetWinnerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AssetWinnerController(IMediator mediator)
        {
            _mediator = mediator;   
        }





        [HttpGet("WinsLIst/{id}")]
        public async Task<ActionResult<List<AssetWinnerDto>>> GetWinsByUserId(int id)
        {
             var query = new GetByUserIdAsyncQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("NotifiationList/{id}")]
        public async Task<ActionResult<List<AssetWinnerDto>>> GetUnseenWinsByUserId(int id)
        {
            var query = new GetUnseenWinsByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPut("mark-as-seen/{id}")]
        public async Task<ActionResult<int>> MarkWinAsSeen(int id)
        {
            var command = new MarkWinsAsSeenCommand(id);

            var result = await _mediator.Send(command);
            return Ok(new
            {
                message = "Marked successfully",
                affectedRows = result
            });

        }
    }
}
