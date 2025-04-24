using FluentValidation;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;

namespace AuctionManagementSystem.Application.Features.Transaction.Commands.CreateTransaction;

public class CreateTransactionDtoValidator : AbstractValidator<CreateTransactionDto>
{
    public CreateTransactionDtoValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User is required");

        RuleFor(x => x.TransactionTypeId)
            .GreaterThan(0).WithMessage("Transaction type is required");

        RuleFor(x => x.PaymentMethodId)
            .GreaterThan(0).WithMessage("Payment method is required");

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("Status is required");

        RuleFor(x => x.MerchantTransactionId)
            .MaximumLength(100).WithMessage("Merchant transaction ID too long");

        RuleFor(x => x.CardTypeId)
            .GreaterThan(0).When(x => x.CardTypeId.HasValue).WithMessage("Invalid card type");

        RuleFor(x => x.TransactionDateTime)
            .NotEmpty().WithMessage("Transaction date/time is required");
    }
}
