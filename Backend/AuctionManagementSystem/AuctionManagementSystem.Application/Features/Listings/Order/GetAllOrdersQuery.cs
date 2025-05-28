using MediatR;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;

public class GetAllOrdersQuery : IRequest<List<DirectSaleAssetDto>>
{
    public int UserId { get; set; }

    public GetAllOrdersQuery(int userId)
    {
        UserId = userId;
    }

    public class CreateOrderWithTransactionCommand : IRequest<List<DirectSaleAssetDto>>
    {
        public int UserId { get; set; }
        public List<int> AssetIds { get; set; } = new();
        public string paymenttype { get; set; } = string.Empty;
    }
}
