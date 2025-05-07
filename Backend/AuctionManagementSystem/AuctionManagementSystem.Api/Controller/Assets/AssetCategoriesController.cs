using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.DTOs.Assets.AssetCategory;
using AuctionManagementSystem.Application.Features.Assets.AssetCategory.Command.CreateAssetCategory;
using AuctionManagementSystem.Application.Features.Assets.AssetCategory.Query.GetAllAssetCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.Api.Controller.Assets
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetCategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssetCategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<AssetCategoryDto>>> GetAllAssetCategories()
        {
            var result = await _mediator.Send(new GetAllAssetCategoriesQuery());
            return Ok(result);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAssetCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateAssetCategoryCommand(dto);
            var newCategoryId = await _mediator.Send(command);
            return Ok(new { categoryId = newCategoryId });
        }
    }

}
