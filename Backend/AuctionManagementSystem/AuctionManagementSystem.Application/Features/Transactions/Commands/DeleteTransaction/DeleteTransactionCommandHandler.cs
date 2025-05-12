using MediatR;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Features.Transactions.Commands.DeleteTransaction;

namespace AuctionManagementSystem.Application.Features.Transaction.Commands.DeleteTransaction;

public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, bool>
{
    private readonly ITransactionRepository _repository;

    public DeleteTransactionCommandHandler(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.TransactionId);
        if (entity == null || entity.IsDeleted)
            return false;

        entity.IsDeleted = true; // Mark as soft-deleted
        await _repository.UpdateAsync(entity); // Save updated entity
        return true;
    }
}
