using MediatR;
using Microsoft.AspNetCore.Mvc;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using AuctionManagementSystem.Application.Features.Transactions.Commands.CreateTransaction;
using AuctionManagementSystem.Application.Features.Transactions.Commands.DeleteTransaction;
using AuctionManagementSystem.Application.Features.Transactions.Commands.UpdateTransaction;
using AuctionManagementSystem.Application.Features.Transactions.Queries.GetTransactionById;
using AuctionManagementSystem.Application.Features.Transactions.Queries.GetAllTransactions;
using AuctionManagementSystem.Application.Features.Transactions;

namespace AuctionManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[HttpGet(Name ="GetAll Transactions")]
    //public async Task<ActionResult<List<TransactionDto>>> GetAll()
    //{
    //    var result = await _mediator.Send(new GetAllTransactionsQuery());
    //    return Ok(result);
    //}

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllTransactionsQuery());
        return Ok(result);
    }


    [HttpGet("{id}",Name ="Get Transaction")]
    //public async Task<ActionResult<TransactionDto>> GetById(int id)
    //{
    //    var result = await _mediator.Send(new GetTransactionByIdQuery { TransactionId = id });
    //    return Ok(result);
    //}

    //[HttpGet("{id}")]
    public async Task<ActionResult<TransactionDto>> GetTransactionById(int id)
    {
        var query = new GetTransactionByIdQuery(id);
        var transaction = await _mediator.Send(query);

        if (transaction == null)
        {
            return NotFound(new { message = "Transaction not found" });
        }

        return Ok(transaction);
    }

    [HttpPost(Name ="Create Transaction")]
    public async Task<ActionResult<TransactionDto>> Create([FromBody] CreateTransactionDto dto)
    {
        var command = new CreateTransactionCommand { Transaction = dto };
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTransactionById), new { id = result.TransactionId }, result);
    }

    [HttpPut("{id}", Name ="Update Transaction")]
    public async Task<ActionResult<TransactionDto>> Update(int id, [FromBody] UpdateTransactionDto dto)
    {
        if (id != dto.TransactionId)
            return BadRequest("Transaction ID mismatch.");

        var command = new UpdateTransactionCommand { Transaction = dto };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}",Name ="Delete Transaction")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteTransactionCommand { TransactionId = id });
        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpGet("metadata")]
    public async Task<IActionResult> GetMetadata()
    {
        var result = await _mediator.Send(new GetTransactionMetadataQuery());
        return Ok(result);
    }
}
