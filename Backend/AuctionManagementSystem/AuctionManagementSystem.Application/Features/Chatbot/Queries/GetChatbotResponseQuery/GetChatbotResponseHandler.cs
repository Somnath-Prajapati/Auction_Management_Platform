using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Chatbot;
using AuctionManagementSystem.Application.Dtos.Chatbot;
using AuctionManagementSystem.Application.Services;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Chatbot.Queries.GetChatbotResponseQuery
{
    public class GetChatbotResponseHandler : IRequestHandler<GetChatbotResponseQuery, ChatbotResponseDto>
    {
        private readonly IChatbotRepository _chatbotRepository;
        private readonly ChatBotService _chatBotService;

        public GetChatbotResponseHandler(IChatbotRepository chatbotRepository)
        {
            _chatbotRepository = chatbotRepository;
            _chatBotService = new ChatBotService(chatbotRepository);
        }

        public async Task<ChatbotResponseDto> Handle(GetChatbotResponseQuery request, CancellationToken cancellationToken)
        {
            return await _chatBotService.GetResponseForUserMessageAsync(request.Message);
        }
    }
}
