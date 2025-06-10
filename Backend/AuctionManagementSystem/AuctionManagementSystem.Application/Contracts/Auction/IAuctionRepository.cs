using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities;
using AuctionManagementSystem.Domain.Entities.Auction;


namespace AuctionManagementSystem.Application.Contracts
{
    public interface IAuctionRepository
    {
        Task<IEnumerable<TblAuction>> GetAllAsync();
        Task<TblAuction> GetByIdAsync(int auctionId);
        Task AddAsync(TblAuction auction);
        void Update(TblAuction auction);
        void Delete(TblAuction auction);
        Task<List<TblAuction>> GetAuctionsByIdsAsync(List<int> auctionIds);

    }
}
