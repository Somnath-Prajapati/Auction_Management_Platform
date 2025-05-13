using AutoMapper;
using MediatR;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using AuctionManagementSystem.Application.Features.Transactions.Queries.GetTransactionById;
using AuctionManagementSystem.Application.Exceptions;

namespace AuctionManagementSystem.Application.Features.Transaction.Queries.GetTransactionById;

public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto>
{
    private readonly ITransactionRepository _repository;
    private readonly IMapper _mapper;

    public GetTransactionByIdQueryHandler(ITransactionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TransactionDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var transaction = await _repository
             .GetTransactionByIdAsync(request.TransactionId);

        if (transaction == null)
        {
            throw new NotFoundException("Transaction not found.");
        }

        var transactionDto = _mapper.Map<TransactionDto>(transaction);
        return transactionDto;
    }
}
