using AuctionManagementSystem.Application.Features.Reports;
using AuctionManagementSystem.Application.Features.Reports.GetHighBiddingCustomersQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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


        [HttpGet("get-directsale-assets")]
        public async Task<IActionResult> GetDirectSaleAssets()
        {
            var query = new GetDirectSaleAssetsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("account-statement")]
        public async Task<IActionResult> GetStatementAccount([FromQuery] int? userId, [FromQuery] int? statusId)
        {
            var query = new GetStatementAccountQuery
            {
                UserId = userId,
                StatusId = statusId
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("get-refund-request")]
        public async Task<IActionResult> GetRefundRequest()
        {
            var query = new GetRefundRequestQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        

        [HttpGet("get-latest-deposit")]
        public async Task<IActionResult> GetLatestDepositRequest()
        {
            var query = new GetLatestDepositQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}


