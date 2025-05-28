using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using AuctionManagementSystem.Application.Features.Transactions.Commands.UpdateTransaction;
using Newtonsoft.Json;

namespace AuctionManagementSystem.Application.Features.Transaction.Commands.UpdateTransaction
{
    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, TransactionDto>
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuditTrailService _auditTrailService;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<UpdateTransactionCommandHandler> _logger;

        public UpdateTransactionCommandHandler(
            ITransactionRepository repository,
            IMapper mapper,
            IAuditTrailService auditTrailService,
            ICurrentUserService currentUser,
            ILogger<UpdateTransactionCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _auditTrailService = auditTrailService;
            _currentUser = currentUser;
            _logger = logger;
        }

        public async Task<TransactionDto> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existing = await _repository.GetByIdAsync(request.Transaction.TransactionId);
                if (existing == null)
                {
                    _logger.LogWarning("Transaction ID {TransactionId} not found for update.", request.Transaction.TransactionId);
                    throw new KeyNotFoundException("Transaction not found.");
                }

                var beforeChange = existing.Clone(); // Clone to capture before state

                _mapper.Map(request.Transaction, existing);
                await _repository.UpdateAsync(existing);
                await _repository.HandleDepositAdjustmentOnStatusChangeAsync(beforeChange, existing);

                _logger.LogInformation("Transaction ID {TransactionId} updated by {Username} (UserId: {UserId})",
                    existing.TransactionId, _currentUser.Username, _currentUser.UserId);

                await _auditTrailService.LogChangeAsync(
                    userId: _currentUser.UserId,
                    username: _currentUser.Username,
                    roleName: _currentUser.RoleName,
                    modelName: "Transaction",
                    changeType: "Update",
                    recordId: existing.TransactionId,
                    beforeChange: JsonConvert.SerializeObject(beforeChange, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }),
                    afterChange: JsonConvert.SerializeObject(existing, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    })
                );

                return _mapper.Map<TransactionDto>(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating Transaction ID {TransactionId}", request.Transaction.TransactionId);
                throw new ApplicationException($"Failed to update transaction. Inner: {ex.Message}", ex);
            }
        }
    }
}
