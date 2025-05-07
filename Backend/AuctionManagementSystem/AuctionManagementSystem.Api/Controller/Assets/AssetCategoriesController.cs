using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Features.Assets.AssetCategory.Command.CreateAssetCategory;
using AuctionManagementSystem.Application.Features.Assets.AssetCategory.Command.DeleteAssetCategory;
using AuctionManagementSystem.Application.Features.Assets.AssetCategory.Command.UpdateAssetCategory;
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
        public async Task<IActionResult> Create([FromForm] CreateAssetCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateAssetCategoryCommand(dto);
            var newCategoryId = await _mediator.Send(command);
            return Ok(new { categoryId = newCategoryId });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssetCategory(int id)
        {
            var command = new DeleteAssetCategoryCommand { CategoryId = id };
            var result = await _mediator.Send(command);
            return result ? Ok() : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromForm] UpdateAssetCategoryDto dto)
        {
            var command = new UpdateAssetCategoryCommand(id, dto);
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound("Category not found or already deleted.");

            return NoContent();
        }


    }

}
