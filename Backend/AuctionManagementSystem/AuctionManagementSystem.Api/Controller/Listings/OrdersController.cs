using AuctionManagementSystem.Application.Dtos.Assets;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static GetAllOrdersQuery;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<DirectSaleAssetDto>>> GetAllOrders(int userId)
    {
        var result = await _mediator.Send(new GetAllOrdersQuery(userId));
        return Ok(result);
    }


    [HttpPost("create-order")]
    public async Task<IActionResult> CreateFromTransaction([FromBody] CreateOrderWithTransactionCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = "An unexpected error occurred.",
                message = ex.Message,
                stackTrace = ex.StackTrace
            });
        }
    }




}
