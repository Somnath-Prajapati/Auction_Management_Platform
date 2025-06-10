using System.Text;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Domain.Entities.User;
using MediatR;

namespace AuctionManagementSystem.Application.Features.AuthFeatures.Command.GenrateOtp
{
    public class GenerateOtpCommandHandler : IRequestHandler<GenerateOtpCommand, bool>
    {
        private readonly IUnitOfWorkAuth _unitOfWork;
        private readonly IEmailService _emailService;

        public GenerateOtpCommandHandler(IUnitOfWorkAuth unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<bool> Handle(GenerateOtpCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
                return false;

            var otpCode = new Random().Next(100000, 999999).ToString();

            var otp = new tblOTP
            {
                UserId = user.UserId,
                Code = otpCode,
                Expiration = DateTime.UtcNow.AddMinutes(5),
                IsUsed = false
            };

            await _unitOfWork.OtpRepository.AddOtpAsync(otp);
            await _unitOfWork.SaveChangesAsync();

            var body = new StringBuilder();
            body.AppendLine("<html><body>");
            body.AppendLine($"<p>Hello,</p>");
            body.AppendLine($"<p>Your OTP is:</p>");
            body.AppendLine($"<h2>{otpCode}</h2>");
            body.AppendLine("<p>This code will expire in 5 minutes.</p>");
            body.AppendLine("</body></html>");


            await _emailService.SendEmailAsync(request.Email, "Your OTP Code", body.ToString());

            return true;
        }
    }
}
