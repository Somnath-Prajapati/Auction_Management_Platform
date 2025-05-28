using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Transactions
{
    public class GetTransactionMetadataHandler : IRequestHandler<GetTransactionMetadataQuery, TransactionMetadataDto>
    {
        private readonly ICardTypeRepository _cardTypeRepo;
        private readonly ITransactionTypeRepository _transactionTypeRepo;
        private readonly IPaymentMethodRepository _paymentMethodRepo;
        private readonly ITransactionStatusRepository _statusRepo;
        private readonly IMapper _mapper;

        public GetTransactionMetadataHandler(
            ICardTypeRepository cardTypeRepo,
            ITransactionTypeRepository transactionTypeRepo,
            IPaymentMethodRepository paymentMethodRepo,
            ITransactionStatusRepository statusRepo,
            IMapper mapper)
        {
            _cardTypeRepo = cardTypeRepo;
            _transactionTypeRepo = transactionTypeRepo;
            _paymentMethodRepo = paymentMethodRepo;
            _statusRepo = statusRepo;
            _mapper = mapper;
        }

        public async Task<TransactionMetadataDto> Handle(GetTransactionMetadataQuery request, CancellationToken cancellationToken)
        {
            return new TransactionMetadataDto
            {
                CardTypes = _mapper.Map<List<CardTypeDto>>(await _cardTypeRepo.GetAllAsync()),
                TransactionTypes = _mapper.Map<List<TransactionTypeDto>>(await _transactionTypeRepo.GetAllAsync()),
                PaymentMethods = _mapper.Map<List<PaymentMethodDto>>(await _paymentMethodRepo.GetAllAsync()),
                Statuses = _mapper.Map<List<TransactionStatusDto>>(await _statusRepo.GetAllAsync())
            };
        }
    }

}
