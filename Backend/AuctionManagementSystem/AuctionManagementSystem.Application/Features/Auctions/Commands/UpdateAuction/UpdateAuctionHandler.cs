using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Application.Features.Auctions.Commands.UpdateAuction;
using AuctionManagementSystem.Domain.Entities.Auction;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;

public class UpdateAuctionHandler : IRequestHandler<UpdateAuctionCommand, bool>
{
    private readonly IAuctionUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAuditTrailService _auditTrailService;
    private readonly ICurrentUserService _currentUser;

    public UpdateAuctionHandler(
        ICurrentUserService currentUser,
        IAuctionUnitOfWork unitOfWork,
        IMapper mapper,
        IAuditTrailService auditTrailService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _auditTrailService = auditTrailService;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(UpdateAuctionCommand request, CancellationToken cancellationToken)
    {
        var auction = await _unitOfWork.AuctionRepository.GetByIdAsync(request.AuctionId);
        if (auction == null)
            return false;

        var oldAuction = JsonConvert.DeserializeObject<TblAuction>(JsonConvert.SerializeObject(auction));

        _mapper.Map(request, auction);
        _unitOfWork.AuctionRepository.Update(auction);
        await _unitOfWork.SaveAsync();

        // Direct audit logging
        await _auditTrailService.LogChangeAsync(
            userId: _currentUser.UserId,
            username: _currentUser.Username,
            roleName: _currentUser.RoleName,
            modelName: "Auction",
            changeType: "Update",
            recordId: auction.AuctionId,
            beforeChange: JsonConvert.SerializeObject(oldAuction, Formatting.Indented),
            afterChange: JsonConvert.SerializeObject(auction, Formatting.Indented)

        );

        Console.WriteLine("CurrentUser: TASK DONE SUCCESSFULLY");
        return true;
    }
}
