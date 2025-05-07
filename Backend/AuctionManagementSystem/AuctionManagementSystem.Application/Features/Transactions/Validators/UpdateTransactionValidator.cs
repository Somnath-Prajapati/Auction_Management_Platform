using FluentValidation;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using AuctionManagementSystem.Application.Features.Transaction.Commands.CreateTransaction;

namespace AuctionManagementSystem.Application.Features.Transaction.Commands.UpdateTransaction;

public class UpdateTransactionDtoValidator : AbstractValidator<UpdateTransactionDto>
{
    public UpdateTransactionDtoValidator()
    {
        RuleFor(x => x.TransactionId)
            .GreaterThan(0).WithMessage("Transaction ID is required");

        //Include(new CreateTransactionDtoValidator());
    }
}
