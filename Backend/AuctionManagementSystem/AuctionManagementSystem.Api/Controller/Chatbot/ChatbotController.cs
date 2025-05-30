using AuctionManagementSystem.Application.Dtos.Chatbot;
using AuctionManagementSystem.Application.Features.Chatbot.Queries.GetChatbotResponseQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.Api.Controller.Chatbot
{

    [ApiController]
    [Route("api/[controller]")]
    public class ChatbotController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatbotController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("message")]
        public async Task<ActionResult<ChatbotResponseDto>> PostMessage([FromBody] ChatbotMessageRequest request)
        {
            var query = new GetChatbotResponseQuery(request.Message, request.UserId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }

    public class ChatbotMessageRequest
    {
        public string Message { get; set; }
        public int? UserId { get; set; }
    }
}
