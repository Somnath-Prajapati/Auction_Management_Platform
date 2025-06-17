using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Dtos.Assets;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Application.Features.Assets.AssetResults
{
    public class GetAssetTransactionQueryHandler : IRequestHandler<GetAssetTransactionQuery, List<AssetTransactionDto>>
    {
        private readonly ITransactionRepository _repository;

        public GetAssetTransactionQueryHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AssetTransactionDto>> Handle(GetAssetTransactionQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAssetTransactionsAsync(request.AssetId);
        }
    }


}
