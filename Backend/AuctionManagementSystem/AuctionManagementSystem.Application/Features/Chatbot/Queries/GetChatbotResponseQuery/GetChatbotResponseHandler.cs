using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Chatbot;
using AuctionManagementSystem.Application.Dtos.Chatbot;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Chatbot.Queries.GetChatbotResponseQuery
{
    public class GetChatbotResponseHandler : IRequestHandler<GetChatbotResponseQuery, ChatbotResponseDto>
    {
        private readonly IChatbotRepository _chatbotRepository;

        public GetChatbotResponseHandler(IChatbotRepository chatbotRepository)
        {
            _chatbotRepository = chatbotRepository;
        }

        public async Task<ChatbotResponseDto> Handle(GetChatbotResponseQuery request, CancellationToken cancellationToken)
        {
            return await _chatbotRepository.GetChatbotResponseAsync(request.Message, request.UserId);
        }
    }
}
