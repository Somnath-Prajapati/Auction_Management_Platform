using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Chatbot;
using AuctionManagementSystem.Domain.Entities;

namespace AuctionManagementSystem.Application.Contracts.Chatbot
{
    public interface IChatbotRepository
    {
        Task<ChatbotResponseDto> GetChatbotResponseAsync(string userMessage, int? userId);
        Task<List<TblChatBotMainQuestionAnswer>> GetAllMainQuestionsAsync();
        Task<List<TblchatbotSubQuestionAnswer>> GetAllSubQuestionsAsync();
        Task<List<TblchatbotSubQuestionAnswer>> GetSubQuestionsByMainIdAsync(int mainId);
        Task<List<TblchatbotSubQuestionAnswer>> GetSubQuestionsByParentIdAsync(int parentSubId);
        Task<TblChatBotMainQuestionAnswer> FindMainQuestionByKeywordAsync(string keyword);
        Task<TblchatbotSubQuestionAnswer> FindSubQuestionByKeywordAsync(string keyword);
        Task<TblChatBotMainQuestionAnswer> GetMainQuestionByIdAsync(int id);
        Task<TblchatbotSubQuestionAnswer> GetSubQuestionByIdAsync(int id);
    }
}
