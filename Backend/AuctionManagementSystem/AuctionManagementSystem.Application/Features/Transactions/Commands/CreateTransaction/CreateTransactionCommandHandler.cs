using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using AuctionManagementSystem.Application.Features.Transactions.Commands.CreateTransaction;
using AuctionManagementSystem.Domain.Entities.Transaction;
using AutoMapper;
using MediatR;

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
        // Map the CreateTransactionCommand to TblTransaction entity
        var entity = _mapper.Map<TblTransaction>(request.Transaction);

        // Assign a unique transaction number (fetch it from repository)
        entity.TransactionNumber = await _repository.GetTransactionNumberFromDbAsync();

        // Save the entity to the database
        await _repository.AddAsync(entity);

        // Fetch the saved transaction including related data using repository method
        var savedEntity = await _repository.GetTransactionWithDetailsAsync(entity.TransactionId);

        // Map the saved entity to TransactionDto
        var transactionDto = _mapper.Map<TransactionDto>(savedEntity);

        // Return the DTO
        return transactionDto;
    }
}
