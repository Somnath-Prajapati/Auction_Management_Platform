using System.Collections.Generic;

namespace AuctionManagementSystem.Application.Dtos.Chatbot
{
    public class ChatbotQATreeDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public List<ChatbotQATreeDto> SubQuestions { get; set; }
    }
} 