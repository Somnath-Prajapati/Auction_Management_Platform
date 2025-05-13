using AuctionManagementSystem.Application.Features.Listings.Cart;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.Api.Controller.Listings
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var result = await _mediator.Send(new GetCartByUserIdQuery { UserId = userId });
            return Ok(result);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
         }


        //[HttpPost("decrease")]
        //public async Task<IActionResult> DecreaseQuantity([FromBody] DecreaseCartItemQuantityCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    if (result)
        //        return Ok(new { success = true, message = "Quantity decreased successfully." });
        //    else
        //        return NotFound(new { success = false, message = "Item not found or already deleted." });
        //}
    }
}
