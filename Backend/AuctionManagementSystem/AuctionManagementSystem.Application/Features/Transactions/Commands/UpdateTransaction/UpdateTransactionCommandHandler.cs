using AutoMapper;
using MediatR;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using AuctionManagementSystem.Application.Features.Transactions.Commands.UpdateTransaction;

namespace AuctionManagementSystem.Application.Features.Transaction.Commands.UpdateTransaction;

public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, TransactionDto>
{
    private readonly ITransactionRepository _repository;
    private readonly IMapper _mapper;

    public UpdateTransactionCommandHandler(ITransactionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TransactionDto> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Transaction.TransactionId);
        if (existing == null)
            throw new KeyNotFoundException("Transaction not found.");

        _mapper.Map(request.Transaction, existing);
        await _repository.UpdateAsync(existing);
        return _mapper.Map<TransactionDto>(existing);
    }
}
