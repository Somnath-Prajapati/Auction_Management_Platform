using AutoMapper;
using MediatR;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using AuctionManagementSystem.Application.Features.Transactions.Queries.GetAllTransactions;

namespace AuctionManagementSystem.Application.Features.Transaction.Queries.GetTransactionList;

public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, List<GetTransactionDto>>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public GetAllTransactionsQueryHandler(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task<List<GetTransactionDto>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
    {
        var transactions = await _transactionRepository.GetAllWithDetailsAsync(cancellationToken);

        // Exclude soft-deleted transactions
        var activeTransactions = transactions.Where(t => !t.IsDeleted).ToList();

        var result = _mapper.Map<List<GetTransactionDto>>(activeTransactions);

        foreach (var txn in result)
        {
            txn.DocumentUrl = await _transactionRepository
                .GetDocumentPathByTransactionIdAsync(txn.TransactionId, cancellationToken);
        }

        return result;
    }
}



