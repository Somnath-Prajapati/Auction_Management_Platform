using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Features.Transactions;
using AuctionManagementSystem.Domain;
using AuctionManagementSystem.Domain.Entities.Transaction;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.Transactions
{
    public class CardTypeRepository : ICardTypeRepository
    {
        private readonly AuctionManagementDbContext _context;

        public CardTypeRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<TblCardType>> GetAllAsync()
        {
            return await _context.TblCardTypes.OrderBy(x => x.CardTypeId).ToListAsync();
        }
    }

    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly AuctionManagementDbContext _context;

        public PaymentMethodRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<TblPaymentMethod>> GetAllAsync()
        {
            return await _context.TblPaymentMethods.OrderBy(x => x.PaymentMethodId).ToListAsync();
        }
    }

    public class TransactionTypeRepository : ITransactionTypeRepository
    {
        private readonly AuctionManagementDbContext _context;

        public TransactionTypeRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<TblTransactionType>> GetAllAsync()
        {
            return await _context.TblTransactionTypes.OrderBy(x => x.TransactionTypeId).ToListAsync();
        }
    }

    public class TransactionStatusRepository : ITransactionStatusRepository
    {
        private readonly AuctionManagementDbContext _context;

        public TransactionStatusRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<TblTransactionStatus>> GetAllAsync()
        {
            return await _context.TblTransactionStatuses.OrderBy(x => x.StatusId).ToListAsync();
        }
    }





}

