using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Application.Contracts.Listings;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Transaction;

namespace AuctionManagementSystem.Persistence.Repositories.Listings
{
    public class OrderEmailService : IOrderEmailService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public OrderEmailService(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task SendOrderConfirmationEmailAsync(int userId, TblTransaction transaction, List<TblAsset> assets)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null || string.IsNullOrWhiteSpace(user.Email))
                return;

            var assetList = string.Join("<br>", assets.Select(a => $"- {a.Title}"));
            var subject = "Your Order Confirmation";
            var body = $@"
            <p>Dear {user.Name},</p>
            <p>Thank you for your purchase. Your order has been confirmed.</p>
            <p><strong>Transaction Number:</strong> {transaction.TransactionNumber}</p>
            <p><strong>Total Amount:</strong> {transaction.Amount:C}</p>
            <p><strong>Assets:</strong><br>{assetList}</p>
            <p>We appreciate your business!</p>
            <p>- The Auction Team</p>
        ";

            await _emailService.SendEmailAsync(user.Email, subject, body);
        }
    }

}
