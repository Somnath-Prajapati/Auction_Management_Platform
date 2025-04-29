using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.Request;

namespace AuctionManagementSystem.Application.Contracts.Request
{
    public interface IRequestRepository
    {
        Task<IEnumerable<TblRequest>> GetAllRequestQuery();
        Task<TblRequest> GetRequestByIdQuery(int requestId);
        Task AddRequest(TblRequest request);

        Task UpdateRequest(TblRequest request);

        Task DelRequest(TblRequest request);


    }
}