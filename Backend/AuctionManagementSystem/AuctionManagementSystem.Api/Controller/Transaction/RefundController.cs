using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AuctionManagementSystem.Application.Features.Transactions.Commands.CreateTransaction;


namespace AuctionManagementSystem.Api.Controller.Transaction
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class RefundController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RefundController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("trigger")]
        public async Task<IActionResult> TriggerRefund()
        {
            int refundsCreated = await _mediator.Send(new ProcessAutoRefundCommand());
            return Ok(new { RefundsCreated = refundsCreated });
        }
    }

}
