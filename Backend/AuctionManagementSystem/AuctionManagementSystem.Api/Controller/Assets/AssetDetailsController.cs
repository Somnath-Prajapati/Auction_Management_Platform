
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Features.Assets.AssetDetails.Command;
using AuctionManagementSystem.Application.Features.Assets.AssetDetails.Query;
using AuctionManagementSystem.Application.Features.Assets.AssetDetails.Query.GetDetailsById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.Api.Controller.Assets
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssetDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAssetDetails([FromBody] List<AssetDetailDto> assetDetails)
        {
            var command = new CreateAssetDetailsCommand
            {
                AssetDetails = assetDetails
            };

            var result = await _mediator.Send(command);

            if (result == null || !result.Any())
            {
                return BadRequest("Failed to create asset details.");
            }

            return Ok(result);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AssetDetailDto>>> GetAssetDetail(int id)
        {
            var assets = await _mediator.Send(new GetAssetDetailByIdQuery(id));
            return Ok(assets);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetDetailDto>>> GetAssetDetails()
        {
            var assets = await _mediator.Send(new GetAssetDetailQuery());
            return Ok(assets);
        }

    }
}

