using FluentValidation;
using AuctionManagementSystem.Application.Dtos.Settings;

namespace AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.UpdateFinanceSettings
{
    public class UpdateFinanceSettingsCommandValidator : AbstractValidator<FinanceSettingsDto>
    {
        public UpdateFinanceSettingsCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("ID must be greater than 0.");

            RuleFor(x => x.VATPercent)
                .GreaterThanOrEqualTo(0).WithMessage("VAT percent must be greater than or equal to 0.")
                .LessThanOrEqualTo(100).WithMessage("VAT percent cannot exceed 100.");

            RuleFor(x => x.CreditCardFee)
                .GreaterThanOrEqualTo(0).WithMessage("Credit card fee must be greater than or equal to 0.");

            RuleFor(x => x.DebitCardFee)
                .GreaterThanOrEqualTo(0).WithMessage("Debit card fee must be greater than or equal to 0.");

            RuleFor(x => x.AdminFees)
                .GreaterThanOrEqualTo(0).WithMessage("Admin fees must be greater than or equal to 0.");

            RuleFor(x => x.AuctionFees)
                .GreaterThanOrEqualTo(0).WithMessage("Auction fees must be greater than or equal to 0.");

            RuleFor(x => x.BuyerCommissionPercent)
                .GreaterThanOrEqualTo(0).WithMessage("Buyer commission percent must be greater than or equal to 0.")
                .LessThanOrEqualTo(100).WithMessage("Buyer commission percent cannot exceed 100.");
        }
    }
}
