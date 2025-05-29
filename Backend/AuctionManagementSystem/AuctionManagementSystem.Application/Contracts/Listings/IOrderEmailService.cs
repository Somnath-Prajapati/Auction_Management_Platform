using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Transaction;

namespace AuctionManagementSystem.Application.Contracts.Listings
{
    public interface IOrderEmailService
    {
        Task SendOrderConfirmationEmailAsync(int userId, TblTransaction transaction, List<TblAsset> assets);
    }

}
