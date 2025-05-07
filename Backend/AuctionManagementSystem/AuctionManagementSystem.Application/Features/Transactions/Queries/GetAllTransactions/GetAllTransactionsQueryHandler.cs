using AutoMapper;
using MediatR;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using AuctionManagementSystem.Application.Features.Transactions.Queries.GetAllTransactions;

namespace AuctionManagementSystem.Application.Features.Transaction.Queries.GetTransactionList;

//public class GetTransactionQueryHandler : IRequestHandler<GetAllTransactionsQuery, List<TransactionDto>>
//{
//    private readonly ITransactionRepository _repository;
//    private readonly IMapper _mapper;

//    public GetTransactionQueryHandler(ITransactionRepository repository, IMapper mapper)
//    {
//        _repository = repository;
//        _mapper = mapper;
//    }

//    public async Task<List<TransactionDto>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
//    {
//        var list = await _repository.GetAllAsync();
//        return _mapper.Map<List<TransactionDto>>(list);
//    }
//}


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

        return _mapper.Map<List<GetTransactionDto>>(activeTransactions);
    }
}



