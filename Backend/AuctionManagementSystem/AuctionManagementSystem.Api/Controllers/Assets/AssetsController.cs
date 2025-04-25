using System.Runtime.CompilerServices;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Features.Assets.Asset.Command.AddAsset;
using AuctionManagementSystem.Application.Features.Assets.Asset.Command.DeleteAsset;
using AuctionManagementSystem.Application.Features.Assets.Asset.Command.UpdateAsset;
using AuctionManagementSystem.Application.Features.Assets.Asset.Query.GetAssetById;
using AuctionManagementSystem.Application.Features.Assets.Asset.Query.SearchAsset;
using AuctionManagementSystem.Application.Features.Assets.Query.GetAssets;
using AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Query.GetAllFinanceSettings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.Api.Controllers.Assets
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetAssetsDto>>> GetAllAssets()
        {
            var assets = await _mediator.Send(new GetAssetsQuery());
            return Ok(assets);
        }

        [HttpPost]
        public async Task<ActionResult<GetAssetsDto>> CreateAsset(CreateAssetsDto createAsset)
        {
            var result = await _mediator.Send(new AddAssetCommand(createAsset));
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<GetAssetsDto>>> GetAsset(int id)
        {
            var assets = await _mediator.Send(new GetAssetByIdQuery(id));
            return Ok(assets);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UpdateAssetDto>> UpdateAsset(int id,[FromBody] UpdateAssetDto assetsDto)
        {
            if(id != assetsDto.AssetId)
                return BadRequest("ID in URL does not match ID in body");   
            await _mediator.Send(new UpdateAssetCommand(id, assetsDto));
            return Ok();
        }

        [Route("Search Asset")]
        [HttpPost]
        public async Task<ActionResult<UpdateAssetDto>> SearchAsset(string name)
        {
            var assets = await _mediator.Send(new SearchAssetQuery(name));
            return Ok(assets);
        }

      
        [HttpDelete]
        public async Task<ActionResult<UpdateAssetDto>> DeleteAsset(int id)
        {
            await _mediator.Send(new DeleteAssetCommand(id));
            return Ok();
        }

    }
}
