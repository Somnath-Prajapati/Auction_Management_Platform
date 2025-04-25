using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Features.Assets.AssetGallery.Command.AddAssetGallery;
using AuctionManagementSystem.Application.Features.Assets.AssetGallery.Command.DeleteAssetGallery;
using AuctionManagementSystem.Application.Features.Assets.AssetGallery.Command.UpdateAssetGallery;
using AuctionManagementSystem.Application.Features.Assets.AssetGallery.Query.GetAssetGallery;
using AuctionManagementSystem.Application.Features.Assets.AssetGallery.Query.GetAssetGalleryById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.Api.Controllers.Assets
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetGalleryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssetGalleryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] AssetsGalleryDto dto)
        {
            var command = new AddAssetGalleryCommand(dto);
            var id = await _mediator.Send(command);
            return Ok(new { GalleryId = id });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] AssetsGalleryDto dto)
        {
            var result = await _mediator.Send(new UpdateAssetGalleryCommand(id, dto));
            return result ? Ok() : NotFound();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteAssetGalleryCommand(id));
            return result ? Ok() : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _mediator.Send(new GetAllAssetGalleriesQuery());
            return Ok(list);
        }
            
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetAssetGalleryByIdQuery(id));
            return result != null ? Ok(result) : NotFound();
        }
    }
}
