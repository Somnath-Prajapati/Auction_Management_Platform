using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Chatbot
{
    public class ChatbotResponseDto
    {
        public string ResponseMessage { get; set; }
        public List<QuickReplyDto> QuickReplies { get; set; }
        public List<HelpArticleDto> SuggestedArticles { get; set; }
    }

    public class QuickReplyDto
    {
        public string Text { get; set; }
        public string Payload { get; set; }
    }

    public class HelpArticleDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
