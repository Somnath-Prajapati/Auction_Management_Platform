using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Chatbot;

namespace AuctionManagementSystem.Application.Contracts.Chatbot
{
    public interface IChatbotRepository
    {
        Task<ChatbotResponseDto> GetChatbotResponseAsync(string userMessage, int? userId);
    }
}
