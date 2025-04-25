using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Domain.Entities.User;
using MediatR;

namespace AuctionManagementSystem.Application.Features.UserFeature.Query.GetStatus
{
    public class GetAllStatusesQueryHandler : IRequestHandler<GetAllStatusesQuery, IEnumerable<TblUserStatus>>
    {
        private readonly IUserStatusRepository _statusRepository;

        public GetAllStatusesQueryHandler(IUserStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<IEnumerable<TblUserStatus>> Handle(GetAllStatusesQuery request, CancellationToken cancellationToken)
        {
            return await _statusRepository.GetAllAsync();
        }
    }
}