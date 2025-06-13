using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using AuctionManagementSystem.Application.Features.Transactions.Commands.CreateTransaction;
using AuctionManagementSystem.Domain.Entities.Transaction;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionDto>
{
    private readonly ITransactionRepository _repository;
    private readonly IMapper _mapper;
    private readonly IAuditTrailService _auditTrailService;
    private readonly IFileService _fileService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<CreateTransactionCommandHandler> _logger;

    public CreateTransactionCommandHandler(
        ITransactionRepository repository,
        IMapper mapper,
        IAuditTrailService auditTrailService,
        ICurrentUserService currentUser,
        IFileService fileService,
        ILogger<CreateTransactionCommandHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _auditTrailService = auditTrailService;
        _currentUser = currentUser;
        _logger = logger;
        _fileService = fileService;
    }

    public async Task<TransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _mapper.Map<TblTransaction>(request.Transaction);
            entity.TransactionNumber = await _repository.GetTransactionNumberFromDbAsync();
            
            await _repository.AddAsync(entity);

            // 💾 Save associated documents
            if (request.Transaction.Documents != null)
            {
                var file = request.Transaction.Documents;

                if (file.Length > 2 * 1024 * 1024)
                    throw new ArgumentException("Document size must not exceed 2 MB");

                var extension = Path.GetExtension(file.FileName).ToLower();
                if (extension != ".pdf")
                    throw new ArgumentException("Only PDF files are allowed");

                var filePath = await _fileService.SaveFileAsync(file, "TransactionDocuments");

                var document = new TblTransactionDocument
                {
                    TransactionId = entity.TransactionId,
                    DocumentType = "Supporting Document",
                    FilePath = "https://localhost:62627/" + filePath,
                    UploadedAt = DateTime.UtcNow
                };

                await _repository.AddTransactionDocumentsAsync(new List<TblTransactionDocument> { document });
            }



            var savedEntity = await _repository.GetTransactionWithDetailsAsync(entity.TransactionId);
            var transactionDto = _mapper.Map<TransactionDto>(savedEntity);

            await _auditTrailService.LogChangeAsync(
                _currentUser.UserId,
                _currentUser.Username,
                _currentUser.RoleName,
                "Transaction",
                "New",
                entity.TransactionId,
                null,
                JsonConvert.SerializeObject(entity, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));

            _logger.LogInformation("Transaction created by {Username}. ID: {TransactionId}", _currentUser.Username, entity.TransactionId);
            return transactionDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating transaction");
            throw new ApplicationException("Transaction creation failed.", ex);
        }
    }


}
