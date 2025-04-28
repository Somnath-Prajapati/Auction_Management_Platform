using AuctionManagementSystem.Application.Dtos.RequestsDtos;
using AuctionManagementSystem.Application.Features.Requests.Command.AddRequest;
using AuctionManagementSystem.Application.Features.Requests.Command.DelRequest;
using AuctionManagementSystem.Application.Features.Requests.Command.UpdRequest;
using AuctionManagementSystem.Application.Features.Requests.Queries.GetAllRequestById;
using AuctionManagementSystem.Application.Features.Requests.Queries.GetAllRequests;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.Api.Controllers.Request
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;


        public RequestController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRequests()

        {
            var result = await _mediator.Send(new GetAllRequestQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestById(int id)
        {
            var result = await _mediator.Send(new GetRequestByIdQuery(id));
            if (result == null)
                return NotFound("Request not found.");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddRequest([FromBody] CreateRequestDto dto)
        {
            var command = _mapper.Map<AddRequestCommand>(dto);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
         
        public async Task<IActionResult> UpdateRequest(int id, [FromBody] UpdateRequestDto dto)
        {
            if (id != dto.RequestId)
                return BadRequest("Mismatched request ID");

            var command = _mapper.Map<UdpRequestCommand>(dto);
            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound("Request not found.");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var result = await _mediator.Send(new DelRequestCommand(id));
            if (!result)
                return NotFound("Request not found.");
            return Ok("Deleted successfully");
        }
    }
}
