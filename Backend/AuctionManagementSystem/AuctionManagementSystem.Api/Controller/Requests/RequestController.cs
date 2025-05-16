using AuctionManagementSystem.Application.Contracts.Request;
using AuctionManagementSystem.Application.Dtos.Requests;
using AuctionManagementSystem.Application.Features.Requests.Command.AddRequest;
using AuctionManagementSystem.Application.Features.Requests.Command.DelRequest;
using AuctionManagementSystem.Application.Features.Requests.Command.UpdRequest;
using AuctionManagementSystem.Application.Features.Requests.Queries.GetAllRequestById;
using AuctionManagementSystem.Application.Features.Requests.Queries.GetAllRequests;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Api.Controllers.Request
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IRequestRepository _repository;



        public RequestController(IMediator mediator, IMapper mapper, IRequestRepository repository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _repository = repository;
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



        // GET: api/Request/template
        //[HttpGet("template")]
        //public async Task<ActionResult<CreateRequestDto>> GetNewTemplate()
        //{
        //    var nextNumber = await _repository.GenerateRequestNumberAsync();
        //    var now = DateTime.UtcNow;

        //    var dto = new CreateRequestDto
        //    {
        //        RequestNumber = await _repository.GenerateRequestNumberAsync(),
        //        RequestDateTime = now,          
        //        CreatedOn = now,
        //        UpdatedOn = now,
        //        //  defaults (e.g. RequestStatusId = 1 for “Pending”)
        //    };
        //    return Ok(dto);
        //}

        [HttpGet("template")]
        public async Task<IActionResult> GetNewTemplate()
        {
            var nextNumber = await _repository.GenerateRequestNumberAsync();
            var now = DateTime.UtcNow;
            return Ok(new
            {
                requestNumber = nextNumber,
                requestDateTime = now.ToString("yyyy-MM-ddTHH:mm:ss.fff"),
                createdOn = now.ToString("yyyy-MM-ddTHH:mm:ss.fff"),
                updatedOn = now.ToString("yyyy-MM-ddTHH:mm:ss.fff")
                // …any other defaults, as literals or DTO‐mapped…
            });
        }



        [HttpPost("AddRequest")]
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
            var deletedBy = User.Identity?.Name ?? "System";

            var result = await _mediator.Send(new DelRequestCommand
            {
                RequestId = id,
                DeletedBy = deletedBy
            });

            if (!result)
                return NotFound(new { success = false, message = "Request not found." });

            return Ok(new { success = true, message = "Request deleted successfully." });
        }


        //[HttpGet("GetLastRequestNumber")]
        //public async Task<IActionResult> GetLastRequestNumber()
        //{
        //    var lastRequest = await _context.Requests
        //        .OrderByDescending(r => r.RequestNumber)
        //        .FirstOrDefaultAsync();

        //    if (lastRequest == null)
        //        return Ok("REQ000"); // default if no request yet

        //    return Ok(lastRequest.RequestNumber);
        //}

    }
}