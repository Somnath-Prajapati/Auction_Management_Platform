using AuctionManagementSystem.Application.Contracts.Chatbot;
using AuctionManagementSystem.Application.Dtos.Chatbot;
using AuctionManagementSystem.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Services
{
    public class ChatBotService
    {
        private readonly IChatbotRepository _repository;

        public ChatBotService(IChatbotRepository repository)
        {
            _repository = repository;
        }

        // Fetch the full Q&A tree
        public async Task<List<ChatbotQATreeDto>> GetQATreeAsync()
        {
            var mainQuestions = await _repository.GetAllMainQuestionsAsync();
            var subQuestions = await _repository.GetAllSubQuestionsAsync();
            var tree = mainQuestions.Select(mq => BuildTree(mq, subQuestions)).ToList();
            return tree;
        }

        private ChatbotQATreeDto BuildTree(TblChatBotMainQuestionAnswer main, List<TblchatbotSubQuestionAnswer> allSubs)
        {
            var dto = new ChatbotQATreeDto
            {
                Id = main.Id,
                Question = main.Question,
                Answer = main.Answer,
                SubQuestions = allSubs
                    .Where(sq => sq.MainQuestionId == main.Id && sq.ParentSubQuestionId == null)
                    .Select(sq => BuildSubTree(sq, allSubs))
                    .ToList()
            };
            return dto;
        }

        private ChatbotQATreeDto BuildSubTree(TblchatbotSubQuestionAnswer sub, List<TblchatbotSubQuestionAnswer> allSubs)
        {
            return new ChatbotQATreeDto
            {
                Id = sub.Id,
                Question = sub.Question,
                Answer = sub.Answer,
                SubQuestions = allSubs
                    .Where(sq => sq.ParentSubQuestionId == sub.Id)
                    .Select(sq => BuildSubTree(sq, allSubs))
                    .ToList()
            };
        }

        // Enhanced: Tree navigation and leaf detection
        public async Task<ChatbotResponseDto> GetResponseForUserMessageAsync(string userMessage)
        {
            var message = userMessage?.Trim();
            if (string.IsNullOrEmpty(message))
            {
                return new ChatbotResponseDto { ResponseMessage = "Please enter a message.", IsEndOfChat = false };
            }

            // If the message is a number, treat it as a question id (payload)
            if (int.TryParse(message, out int id))
            {
                // Try to find as main question
                var main = (await _repository.GetAllMainQuestionsAsync()).FirstOrDefault(q => q.Id == id);
                if (main != null)
                {
                    var subQuestions = await _repository.GetSubQuestionsByMainIdAsync(main.Id);
                    if (subQuestions.Any())
                    {
                        return new ChatbotResponseDto
                        {
                            ResponseMessage = main.Answer ?? main.Question,
                            QuickReplies = subQuestions.Select(sq => new QuickReplyDto { Text = sq.Question, Payload = sq.Id.ToString() }).ToList(),
                            IsEndOfChat = false
                        };
                    }
                    else
                    {
                        return new ChatbotResponseDto
                        {
                            ResponseMessage = (main.Answer ?? main.Question) + "\nIs there anything else I can help you with?",
                            QuickReplies = new List<QuickReplyDto> { new QuickReplyDto { Text = "Start Over", Payload = "start_over" } },
                            IsEndOfChat = true
                        };
                    }
                }
                // Try to find as sub-question
                var sub = (await _repository.GetAllSubQuestionsAsync()).FirstOrDefault(q => q.Id == id);
                if (sub != null)
                {
                    var subQuestions = await _repository.GetSubQuestionsByParentIdAsync(sub.Id);
                    if (subQuestions.Any())
                    {
                        return new ChatbotResponseDto
                        {
                            ResponseMessage = sub.Answer ?? sub.Question,
                            QuickReplies = subQuestions.Select(sq => new QuickReplyDto { Text = sq.Question, Payload = sq.Id.ToString() }).ToList(),
                            IsEndOfChat = false
                        };
                    }
                    else
                    {
                        return new ChatbotResponseDto
                        {
                            ResponseMessage = (sub.Answer ?? sub.Question) + "\nIs there anything else I can help you with?",
                            QuickReplies = new List<QuickReplyDto> { new QuickReplyDto { Text = "Start Over", Payload = "start_over" } },
                            IsEndOfChat = true
                        };
                    }
                }
            }

            // Greetings
            var greetings = new[] { "hi", "hello", "hey" };
            var lowerMessage = message.ToLower();
            //if (greetings.Any(g => lowerMessage.Contains(g)))
            //{
            //    var mainQuestions = await _repository.GetAllMainQuestionsAsync();
            //    return new ChatbotResponseDto
            //    {
            //        ResponseMessage = "Hello! How can I help you today?",
            //        QuickReplies = mainQuestions.Select(q => new QuickReplyDto { Text = q.Question, Payload = q.Id.ToString() }).ToList(),
            //        IsEndOfChat = false
            //    };
            //}

            if (greetings.Any(g => lowerMessage.Contains(g)))
            {
                var mainQuestions = await _repository.GetAllMainQuestionsAsync();

                // Select only a limited number of questions (e.g., 3 to 5)
                var limitedQuestions = mainQuestions.Take(5).ToList();

                return new ChatbotResponseDto
                {
                    ResponseMessage = "Hello! How can I help you today?",
                    QuickReplies = limitedQuestions.Select(q => new QuickReplyDto { Text = q.Question, Payload = q.Id.ToString() }).ToList(),
                    IsEndOfChat = false
                };
            }


            // Split message into keywords
            var keywords = lowerMessage.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);

            // Search main questions for any keyword match
            var allMainQuestions = await _repository.GetAllMainQuestionsAsync();
            var matchingMain = allMainQuestions
                .Where(q => keywords.Any(k => q.Question.ToLower().Contains(k)))
                .ToList();

            // Search sub-questions for any keyword match
            var allSubQuestions = await _repository.GetAllSubQuestionsAsync();
            var matchingSub = allSubQuestions
                .Where(q => keywords.Any(k => q.Question.ToLower().Contains(k)))
                .ToList();

            // If there are any matches, show as options
            if (matchingMain.Any() || matchingSub.Any())
            {
                var quickReplies = new List<QuickReplyDto>();
                quickReplies.AddRange(matchingMain.Select(q => new QuickReplyDto { Text = q.Question, Payload = q.Id.ToString() }));
                quickReplies.AddRange(matchingSub.Select(q => new QuickReplyDto { Text = q.Question, Payload = q.Id.ToString() }));
                return new ChatbotResponseDto
                {
                    ResponseMessage = $"Let me help! Here are some related topics you can choose from:",
                    QuickReplies = quickReplies,
                    IsEndOfChat = false
                };
            }

            // Name detection logic
            bool looksLikeName = false;
            string name = null;
            if (lowerMessage.StartsWith("my name is "))
            {
                name = message.Substring(11).Trim();
                looksLikeName = true;
            }
            else if (lowerMessage.StartsWith("i am "))
            {
                name = message.Substring(5).Trim();
                looksLikeName = true;
            }
            else if (keywords.Length == 1 && !greetings.Contains(lowerMessage) && !IsQuestionWord(lowerMessage))
            {
                name = message;
                looksLikeName = true;
            }

            if (looksLikeName)
            {
                var mainQuestions = await _repository.GetAllMainQuestionsAsync();
                return new ChatbotResponseDto
                {
                    ResponseMessage = $"Hello, I can help you with our auction platform. Please select an option.",
                    QuickReplies = mainQuestions.Select(q => new QuickReplyDto { Text = q.Question, Payload = q.Id.ToString() }).ToList(),
                    IsEndOfChat = false
                };
            }

            // If not recognized, show greeting and all main questions as options
            {
                var mainQuestions = await _repository.GetAllMainQuestionsAsync();
                return new ChatbotResponseDto
                {
                    ResponseMessage = "Hello, I can help you with our auction platform. Please select an option.",
                    QuickReplies = mainQuestions.Select(q => new QuickReplyDto { Text = q.Question, Payload = q.Id.ToString() }).ToList(),
                    IsEndOfChat = false
                };
            }
        }

        private bool IsQuestionWord(string word)
        {
            var questionWords = new[] { "what", "how", "when", "where", "why", "can", "is", "do", "does", "did", "are", "will", "should" };
            return questionWords.Any(qw => word.Contains(qw));
        }
    }
}