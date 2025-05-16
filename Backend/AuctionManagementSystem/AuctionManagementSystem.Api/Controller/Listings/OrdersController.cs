using AuctionManagementSystem.Application.Dtos.Assets;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
}
