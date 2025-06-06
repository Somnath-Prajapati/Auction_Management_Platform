using AuctionManagementSystem.Application.Features.Bids.AutoBid.Query.GetHighBiddingCustomersQuery;
using AuctionManagementSystem.Application.Features.Transactions;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AuctionManagementSystem.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class Reports : ControllerBase
    {
        private readonly IMediator _mediator;

        public Reports(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("monthly-auction-revenue")]
        public async Task<IActionResult> GetAuctionMonthlyRevenue()
        {
            var result = await _mediator.Send(new GetAuctionRevenueQuery());
            return Ok(result);
        }


        [HttpGet("monthly-directsale-revenue")]
        public async Task<IActionResult> GetDirectSaleMonthlyRevenue()
        {
            var result = await _mediator.Send(new GetDirectSaleRevenueQuery());
            return Ok(result);
        }



        [HttpGet("high-bidding-customers")]
        public async Task<IActionResult> GetHighBiddingCustomers([FromQuery] int? userId)
        {
            var query = new GetHighBiddingCustomersQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
