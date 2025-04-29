using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Contracts
{
    public interface IUnitOfWork
    {
        IAuctionRepository AuctionRepository { get; }
        Task<int> SaveAsync();
    }
}
