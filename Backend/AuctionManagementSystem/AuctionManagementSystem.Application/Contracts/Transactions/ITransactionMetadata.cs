using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain;
using AuctionManagementSystem.Domain.Entities.Transaction;

namespace AuctionManagementSystem.Application.Contracts.Transactions
{
    public interface ICardTypeRepository
    {
        Task<List<TblCardType>> GetAllAsync();
    }
    public interface IPaymentMethodRepository
    {
        Task<List<TblPaymentMethod>> GetAllAsync();
    }

    public interface ITransactionTypeRepository
    {
        Task<List<TblTransactionType>> GetAllAsync();
    }
    public interface ITransactionStatusRepository
    {
        Task<List<TblTransactionStatus>> GetAllAsync();
    }


}
