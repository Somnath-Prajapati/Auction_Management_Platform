using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Application.Features.Transactions.Commands.DeleteTransaction;
using Newtonsoft.Json;

namespace AuctionManagementSystem.Application.Features.Transaction.Commands.DeleteTransaction
{
    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, bool>
    {
        private readonly ITransactionRepository _repository;
        private readonly IAuditTrailService _auditTrailService;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<DeleteTransactionCommandHandler> _logger;

        public DeleteTransactionCommandHandler(
            ITransactionRepository repository,
            IAuditTrailService auditTrailService,
            ICurrentUserService currentUser,
            ILogger<DeleteTransactionCommandHandler> logger)
        {
            _repository = repository;
            _auditTrailService = auditTrailService;
            _currentUser = currentUser;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(request.TransactionId);

                if (entity == null || entity.IsDeleted)
                {
                    _logger.LogWarning("Delete failed. Transaction with ID {TransactionId} not found or already deleted.", request.TransactionId);
                    return false;
                }

                var beforeChange = entity;

                entity.IsDeleted = true;
                //entity.DeletedBy = _currentUser.UserId.ToString();
                //entity.DeletedOn = DateTime.UtcNow;

                await _repository.UpdateAsync(entity);

                _logger.LogInformation("Transaction with ID {TransactionId} soft-deleted by {Username} (UserId: {UserId})",
                    entity.TransactionId, _currentUser.Username, _currentUser.UserId);

                await _auditTrailService.LogChangeAsync(
                    userId: _currentUser.UserId,
                    username: _currentUser.Username,
                    roleName: _currentUser.RoleName,
                    modelName: "Transaction",
                    changeType: "Delete",
                    recordId: entity.TransactionId,
                    beforeChange: JsonConvert.SerializeObject(beforeChange, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }),
    afterChange: JsonConvert.SerializeObject(entity, new JsonSerializerSettings
    {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    })
                );

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating Transaction ID {TransactionId}", request.TransactionId);
    throw new ApplicationException($"Failed to update transaction. Inner: {ex.Message}", ex);
            }
        }
    }
}
