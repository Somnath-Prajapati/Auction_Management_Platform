using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Chatbot;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Chatbot.Queries.GetChatbotResponseQuery
{
    public record GetChatbotResponseQuery(string Message, int? UserId) : IRequest<ChatbotResponseDto>;
   
}
