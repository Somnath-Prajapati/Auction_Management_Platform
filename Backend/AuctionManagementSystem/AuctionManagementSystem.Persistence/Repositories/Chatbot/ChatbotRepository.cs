using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Chatbot;
using AuctionManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AuctionManagementSystem.Persistence.Context;
using AuctionManagementSystem.Application.Dtos.Chatbot;

namespace AuctionManagementSystem.Persistence.Repositories.Chatbot
{
    public class ChatbotRepository : IChatbotRepository
    {
        private readonly AuctionManagementDbContext _context;

        public ChatbotRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<TblChatBotMainQuestionAnswer>> GetAllMainQuestionsAsync()
        {
            return await _context.TblChatBotMainQuestionAnswers.AsNoTracking().ToListAsync();
        }

        public async Task<List<TblchatbotSubQuestionAnswer>> GetAllSubQuestionsAsync()
        {
            return await _context.TblchatbotSubQuestionAnswers.AsNoTracking().ToListAsync();
        }

        public async Task<List<TblchatbotSubQuestionAnswer>> GetSubQuestionsByMainIdAsync(int mainId)
        {
            return await _context.TblchatbotSubQuestionAnswers
                .AsNoTracking()
                .Where(sq => sq.MainQuestionId == mainId && sq.ParentSubQuestionId == null)
                .ToListAsync();
        }

        public async Task<List<TblchatbotSubQuestionAnswer>> GetSubQuestionsByParentIdAsync(int parentSubId)
        {
            return await _context.TblchatbotSubQuestionAnswers
                .AsNoTracking()
                .Where(sq => sq.ParentSubQuestionId == parentSubId)
                .ToListAsync();
        }

        public async Task<TblChatBotMainQuestionAnswer> FindMainQuestionByKeywordAsync(string keyword)
        {
            return await _context.TblChatBotMainQuestionAnswers
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Question.ToLower().Contains(keyword.ToLower()));
        }

        public async Task<TblchatbotSubQuestionAnswer> FindSubQuestionByKeywordAsync(string keyword)
        {
            return await _context.TblchatbotSubQuestionAnswers
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Question.ToLower().Contains(keyword.ToLower()));
        }

        // Implemented for interface compatibility; use ChatBotService for main logic
        public Task<ChatbotResponseDto> GetChatbotResponseAsync(string userMessage, int? userId)
        {
            throw new System.NotImplementedException("Use ChatBotService for chatbot logic.");
        }

        public async Task<TblChatBotMainQuestionAnswer> GetMainQuestionByIdAsync(int id)
        {
            return await _context.TblChatBotMainQuestionAnswers.AsNoTracking().FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<TblchatbotSubQuestionAnswer> GetSubQuestionByIdAsync(int id)
        {
            return await _context.TblchatbotSubQuestionAnswers.AsNoTracking().FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
