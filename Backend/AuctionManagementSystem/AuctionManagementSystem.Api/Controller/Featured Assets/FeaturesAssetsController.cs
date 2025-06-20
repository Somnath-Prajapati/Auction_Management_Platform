using AuctionManagementSystem.Application.Features.GetFeaturedAssets.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.Api.Controller.Featured_Assets
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeaturesAssetsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FeaturesAssetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedAssets()
        {
            var result = await _mediator.Send(new GetFeaturedAssetsQuery());
            return Ok(result);
        }
    }
}
