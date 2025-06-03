using AuctionManagementSystem.Application.Contracts.Listings;
using AuctionManagementSystem.Application.Dtos.Payment;
using AuctionManagementSystem.Application.Features.Listings.Payment;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace AuctionManagementSystem.Api.Controller.Payment
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateStripeSessionDto dto)
        {


            var command = new CreateStripeCheckoutSessionCommand { StripeSessionDto = dto };
            var sessionId = await _mediator.Send(command);
            return Ok(new { sessionId });
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmStripeOrder([FromQuery] string session_id, [FromQuery] int userId)
        {
            var dto = new ConfirmStripePaymentDto { SessionId = session_id, UserId = userId };
            var command = new ConfirmStripePaymentAndCreateOrderCommand { PaymentDto = dto };
            var result = await _mediator.Send(command);
            return Ok(result);
        [HttpPost("confirm-payment")]
        public async Task<IActionResult> ConfirmPayment([FromBody] ConfirmStripePaymentDto dto)
        {
            var assets = await _mediator.Send(new ConfirmStripePaymentAndCreateOrderCommand { PaymentDto = dto });
            return Ok(assets);
        }

        [HttpGet("check-payment-status")]
        public async Task<IActionResult> CheckPaymentStatus([FromQuery] string sessionId)
        {
            var result = await _mediator.Send(new CheckStripePaymentStatusQuery { SessionId = sessionId });
            return Ok(new { status = result });
        }



    }

}
