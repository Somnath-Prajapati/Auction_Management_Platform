using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Chatbot;
using AuctionManagementSystem.Application.Dtos.Chatbot;

namespace AuctionManagementSystem.Persistence.Repositories.Chatbot
{
    public class ChatbotRepository : IChatbotRepository
    {
        public async Task<ChatbotResponseDto> GetChatbotResponseAsync(string userMessage, int? userId)
        {
            var response = new ChatbotResponseDto();
            var lowerMessage = userMessage.ToLower();

            if (lowerMessage.Contains("hello") || lowerMessage.Contains("hi") || lowerMessage.Contains("hey"))
            {
                response.ResponseMessage = "Choose a category below to get what you are looking for!";
                response.QuickReplies = new List<QuickReplyDto>
                {
                    new() { Text = "Auction Related Query", Payload = "auction" },
                    new() { Text = "Asset Related Query", Payload = "asset" },
                    new() { Text = "Report an Issue", Payload = "report" },
                    new() { Text = "I have another question", Payload = "other" }
                };
                response.SuggestedArticles = new List<HelpArticleDto>();
                return response;
            }

            // Handle category selection
            if (lowerMessage.Contains("auction"))
            {
                response.ResponseMessage =
                    "Here’s everything you need to know about auctions on our platform:\n\n" +
                    "- 🔔 Participate in Manual or Auto Auctions.\n" +
                    "- 💰 Bid manually or set Auto Bid with limit.\n" +
                    "- ⏱ Auctions extend by 5 mins if last bid is under 5 mins.\n" +
                    "- 💳 Deposit required for bidding (Top-up available).\n" +
                    "- 🏆 Winners must complete registration after winning.";
                response.QuickReplies = new List<QuickReplyDto>
                {
                    new() { Text = "Auction Rules", Payload = "auction_rules" },
                    new() { Text = "Payment Methods", Payload = "payment_methods" },
                    new() { Text = "Back to Categories", Payload = "hello" }
                };
                response.SuggestedArticles = GetSuggestedArticles("auction");
                return response;
            }

            //if (lowerMessage.Contains("asset"))
            //{
            //    response.ResponseMessage =
            //        "Assets on our platform include detailed information:\n\n" +
            //        "- 🖼️ Grid/List view with filters and search.\n" +
            //        "- 🔍 View details, pricing, documents, VAT.\n" +
            //        "- 🛒 Add to Watchlist or Cart (15-minute timeout).\n" +
            //        "- 🧾 Checkout supports multiple payment options.";
            //    response.QuickReplies = new List<QuickReplyDto>
            //    {
            //        new() { Text = "How to view asset details", Payload = "asset_details" },
            //        new() { Text = "How to bid on asset", Payload = "bid" },
            //        new() { Text = "Back to Categories", Payload = "hello" }
            //    };
            //    response.SuggestedArticles = GetSuggestedArticles("asset");
            //    return response;
            //}


            if (lowerMessage.Contains("asset_details") || lowerMessage.Contains("how to view asset"))
            {
                response.ResponseMessage =
                    "To view asset details:\n\n" +
                    "- Go to the Home screen and select a category.\n" +
                    "- Tap or click on any asset to open its details page.\n" +
                    "- You’ll see all key info: images, specs, pricing, VAT, and documents.\n" +
                    "- Use search and filters to find the right asset quickly.";
                response.QuickReplies = new List<QuickReplyDto>
    {
        new() { Text = "How to bid on asset", Payload = "bid" },
        new() { Text = "Back to Categories", Payload = "hello" }
    };
                response.SuggestedArticles = new List<HelpArticleDto>
    {
        new() { Title = "View Asset Details", Content = "Click on an asset to see its full description, price, documents, and VAT details. You can also add it to your watchlist or cart." }
    };
                return response;
            }

            if (lowerMessage.Contains("asset"))
            {
                response.ResponseMessage =
                    "Assets on our platform include detailed information:\n\n" +
                    "- 🖼️ Grid/List view with filters and search.\n" +
                    "- 🔍 View details, pricing, documents, VAT.\n" +
                    "- 🛒 Add to Watchlist or Cart (15-minute timeout).\n" +
                    "- 🧾 Checkout supports multiple payment options.";
                response.QuickReplies = new List<QuickReplyDto>
    {
        new() { Text = "How to view asset details", Payload = "asset_details" },
        new() { Text = "How to bid on asset", Payload = "bid" },
        new() { Text = "Back to Categories", Payload = "hello" }
    };
                response.SuggestedArticles = GetSuggestedArticles("asset");
                return response;
            }





            if (lowerMessage.Contains("report"))
            {
                response.ResponseMessage =
                    "To report an issue:\n\n" +
                    "- 📝 Describe your issue including asset or transaction IDs.\n" +
                    "- 📞 Our support team will respond via chat, email, or call.\n" +
                    "You may also reach out via:\n" +
                    "📧 Email Us | 💬 Chat | 📱 Call";
                response.QuickReplies = new List<QuickReplyDto>
                {
                    new() { Text = "Back to Categories", Payload = "hello" }
                };
                response.SuggestedArticles = new List<HelpArticleDto>();
                return response;
            }

            if (lowerMessage.Contains("other"))
            {
                response.ResponseMessage =
                    "I'm here to assist you with:\n\n" +
                    "- 🔑 Sign-Up, Login, and Profile Setup\n" +
                    "- 🛒 Direct Sale Purchase & Checkout\n" +
                    "- 🧾 Deposits, Refunds & Bidding Limits\n" +
                    "Please type your question below!";
                response.QuickReplies = new List<QuickReplyDto>
                {
                    new() { Text = "Back to Categories", Payload = "hello" }
                };
                response.SuggestedArticles = new List<HelpArticleDto>();
                return response;
            }

            // Help articles
            if (lowerMessage.Contains("auction rules"))
            {
                response.ResponseMessage =
                    "Auction Rules:\n\n" +
                    "- All auctions have clear start and end times.\n" +
                    "- Highest bid at end wins the asset.\n" +
                    "- You must register and deposit before bidding.\n" +
                    "- Bids must follow minimum increment values.\n" +
                    "- Timer auto-extends by 5 mins if bids are made near end.";
                response.QuickReplies = new List<QuickReplyDto>
                {
                    new() { Text = "Payment Methods", Payload = "payment_methods" },
                    new() { Text = "Back to Categories", Payload = "hello" }
                };
                response.SuggestedArticles = new List<HelpArticleDto>
                {
                    new() { Title = "Auction Rules", Content = "All auctions are fair. You need registration and deposit to bid. Timer extends if bid is placed near auction end." }
                };
                return response;
            }

           
                if (lowerMessage.Contains("payment methods") || lowerMessage.Contains("payment_methods"))
                //if (lowerMessage.Contains("payment methods"))
            {
                response.ResponseMessage =
                   "💳 Accepted Payment Methods\n\n" +
                     "- Visa & Mastercard: Secure and quick transactions.\n" +
                     "- Bank Transfers: Details shared post-checkout for wire payments.\n" +
                      "- Apple Pay: Supported on iOS devices for fast checkout.\n\n" +
                      "After winning an auction or placing a direct sale order, you will receive detailed payment instructions on your registered email. Ensure your payment method is valid before initiating the purchase.\n\n" +
                        "For any help, contact our support via Email, Chat, or Call.";
                response.QuickReplies = new List<QuickReplyDto>
                {
                    new() { Text = "Auction Rules", Payload = "auction_rules" },
                    new() { Text = "Back to Categories", Payload = "hello" }
                };
                response.SuggestedArticles = new List<HelpArticleDto>
                {
                    new() { Title = "Payment Methods",  Content = "You can make payments using Visa, Mastercard, bank transfers, or Apple Pay. After winning an auction or placing an order, instructions will be shared. Contact support for any issues."}
                };
                return response;
            }

            // Default fallback
            response.ResponseMessage =
                "Here's how I can help you:\n\n" +
                "- 🤖 Ask about Auctions, Assets, or Payments\n" +
                "- 💬 Use menu options or type your query clearly\n\n" +
                "Choose a category to proceed or rephrase your question.";
            response.QuickReplies = GetQuickReplies(lowerMessage);
            response.SuggestedArticles = GetSuggestedArticles(lowerMessage);

            if (userId.HasValue)
            {
                response.ResponseMessage += await GetUserContextualInfo(userId.Value);
            }

            return response;
        }

        private string GetRandomizedAuctionReply()
        {
            var replies = new List<string>
            {
                "You can ask about auction rules, how to participate, or payment methods!",
                "Need help with auctions? I can guide you through bidding, deposits, and more.",
                "Auctions are exciting! Ask me anything about the process, rules, or tips.",
                "Looking for auction details? I can help with registration, bidding, and winning!"
            };
            return replies[new Random().Next(replies.Count)];
        }

        private string GetRandomizedAssetReply()
        {
            var replies = new List<string>
            {
                "You can view asset details, see images, and place bids directly from the asset page.",
                "Assets have detailed descriptions and galleries. Ask me how to find the right asset!",
                "Want to know more about an asset? I can help you with specifications, documents, and more.",
                "Need help with assets? I can guide you to view, compare, and bid on assets easily."
            };
            return replies[new Random().Next(replies.Count)];
        }

        private string GetRandomizedOtherReply()
        {
            var replies = new List<string>
            {
                "Feel free to ask any question! I'm here to help with anything related to our platform.",
                "Have another question? Just type it below and I'll do my best to assist you.",
                "I'm ready to answer any other queries you have. Go ahead and ask!",
                "Your questions are important to us. Please type your query and I'll help you out!"
            };
            return replies[new Random().Next(replies.Count)];
        }

        private string GetBaseResponse(string lowerMessage)
        {
            return "Here's how I can help you:\n\n" +
                   "- 🤖 Ask about Auctions, Assets, or Payments\n" +
                   "- 💬 Use menu options or type your query clearly\n\n" +
                   "Choose a category to proceed or rephrase your question.";
        }

        private List<QuickReplyDto> GetQuickReplies(string lowerMessage)
        {
            return new List<QuickReplyDto> { new QuickReplyDto { Text = "Back to Categories", Payload = "hello" } };
        }

        private List<HelpArticleDto> GetSuggestedArticles(string lowerMessage)
        {
            return new List<HelpArticleDto>();
        }

        private async Task<string> GetUserContextualInfo(int userId)
        {
            await Task.CompletedTask;
            return string.Empty;
        }
    }
}
