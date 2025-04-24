using AutoMapper;
using MediatR;
using AuctionManagementSystem.Domain.Entities.Transaction;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using AuctionManagementSystem.Application.Features.Transactions.Commands.CreateTransaction;

namespace AuctionManagementSystem.Application.Features.Transaction.Commands.CreateTransaction;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionDto>
{
    private readonly ITransactionRepository _repository;
    private readonly IMapper _mapper;

    public CreateTransactionCommandHandler(ITransactionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TblTransaction>(request.Transaction);
        var result = await _repository.AddAsync(entity);
        return _mapper.Map<TransactionDto>(result);
    }
}
