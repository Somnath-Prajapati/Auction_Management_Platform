using AuctionManagementSystem.Application.Contracts.Listings;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static GetAllOrdersQuery;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<DirectSaleAssetDto>>
{
    private readonly IOrderRepository _orderRepository;

    public GetAllOrdersQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<DirectSaleAssetDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetAllOrders(request.UserId);
    }
}

public class CreateOrderWithTransactionCommandHandler : IRequestHandler<CreateOrderWithTransactionCommand, List<DirectSaleAssetDto>>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderWithTransactionCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<DirectSaleAssetDto>> Handle(CreateOrderWithTransactionCommand request, CancellationToken cancellationToken)
    {
        return await _orderRepository.ConfirmPaymentAndCreateOrderAsync(request.UserId, request.AssetIds, request.paymenttype, request.amountPaid);
    }
}

