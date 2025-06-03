using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using AuctionManagementSystem.Application.Features.Transactions.Commands.CreateTransaction;
using AuctionManagementSystem.Domain.Entities.Transaction;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using Newtonsoft.Json;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionDto>
{
    private readonly ITransactionRepository _repository;
    private readonly IMapper _mapper;
    private readonly IAuditTrailService _auditTrailService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<CreateTransactionCommandHandler> _logger;

    public CreateTransactionCommandHandler(
        ITransactionRepository repository,
        IMapper mapper,
        IAuditTrailService auditTrailService,
        ICurrentUserService currentUser,
        ILogger<CreateTransactionCommandHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _auditTrailService = auditTrailService;
        _currentUser = currentUser;
        _logger = logger;
    }

    public async Task<TransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _mapper.Map<TblTransaction>(request.Transaction);

            entity.TransactionNumber = await _repository.GetTransactionNumberFromDbAsync();
            //entity.CreatedBy = _currentUser.UserId.ToString();
            //entity.CreatedOn = DateTime.UtcNow;

            await _repository.AddAsync(entity);

            var savedEntity = await _repository.GetTransactionWithDetailsAsync(entity.TransactionId);
            var transactionDto = _mapper.Map<TransactionDto>(savedEntity);

            _logger.LogInformation("Transaction created by {Username} (ID: {UserId}) with role {Role}. TransactionId: {TransactionId}",
                _currentUser.Username, _currentUser.UserId, _currentUser.RoleName, entity.TransactionId);

            await _auditTrailService.LogChangeAsync(
                userId: _currentUser.UserId,
                username: _currentUser.Username,
                roleName: _currentUser.RoleName,
                modelName: "Transaction",
                changeType: "New",
                recordId: entity.TransactionId,
                beforeChange: null,
                afterChange: JsonConvert.SerializeObject(entity, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })
            );

            return transactionDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating Transaction ID {TransactionId}", request.Transaction.TransactionTypeId);
            throw new ApplicationException($"Failed to update transaction. Inner: {ex.Message}", ex);
        }
    }
}
