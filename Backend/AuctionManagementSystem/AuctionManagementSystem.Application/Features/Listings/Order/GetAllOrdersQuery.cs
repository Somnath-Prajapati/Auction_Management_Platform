using MediatR;
using AuctionManagementSystem.Application.Dtos.Assets;

public class GetAllOrdersQuery : IRequest<List<DirectSaleAssetDto>>
{
    public int UserId { get; set; }

    public GetAllOrdersQuery(int userId)
    {
        UserId = userId;
    }
}
