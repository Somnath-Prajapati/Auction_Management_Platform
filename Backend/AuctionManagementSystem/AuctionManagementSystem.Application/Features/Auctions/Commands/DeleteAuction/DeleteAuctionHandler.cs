using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Auctions.Commands.DeleteAuction
{
    public class DeleteAuctionHandler : IRequestHandler<DeleteAuctionCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAuctionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteAuctionCommand request, CancellationToken cancellationToken)
        {
            var auction = await _unitOfWork.AuctionRepository.GetByIdAsync(request.AuctionId);
            if (auction != null)
            {
                _unitOfWork.AuctionRepository.Delete(auction);
                await _unitOfWork.SaveAsync();
            }
            return auction!= null;
        }
    }
}
