
using AuctionManagementSystem.Application.Dtos.Chatbot;
using AuctionManagementSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace AuctionManagementSystem.Api.Controller.Chatbot
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatbotController : ControllerBase
    {
        private readonly ChatBotService _chatBotService;

        public ChatbotController(ChatBotService chatBotService)
        {
            _chatBotService = chatBotService;
        }

        // GET: api/Chatbot/tree
        [HttpGet("tree")]
        public async Task<ActionResult<List<ChatbotQATreeDto>>> GetQATree()
        {
            var tree = await _chatBotService.GetQATreeAsync();
            return Ok(tree);
        }

        // POST: api/Chatbot/message
        [HttpPost("message")]
        public async Task<ActionResult<ChatbotResponseDto>> PostMessage([FromBody] ChatbotMessageRequest request)
        {
            // If message is null or empty, return a rich welcome message with categories
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                var mainQuestions = await _chatBotService.GetQATreeAsync();
                var limitedQuestions = mainQuestions.Take(5).ToList();
                var welcome = new ChatbotResponseDto
                {
                    ResponseMessage = "Hello! Welcome to Mazad Auction Platform. How can I help you today?",
                    QuickReplies = limitedQuestions.Select(q => new QuickReplyDto { Text = q.Question, Payload = q.Id.ToString() }).ToList()
                };
                return Ok(welcome);
            }

            // Otherwise, use the service to get a response from the database
            var result = await _chatBotService.GetResponseForUserMessageAsync(request.Message);
            return Ok(result);
        }
    }

    public class ChatbotMessageRequest
    {
        public string Message { get; set; }
        public int? UserId { get; set; }
    }
}
