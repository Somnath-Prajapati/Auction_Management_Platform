using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.Bids;
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
    private readonly IAuctionJobScheduler _jobScheduler;

    public UpdateAuctionHandler(
        ICurrentUserService currentUser,
        IAuctionUnitOfWork unitOfWork,
        IMapper mapper,
         IAuctionJobScheduler jobScheduler,
        IAuditTrailService auditTrailService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _auditTrailService = auditTrailService;
        _currentUser = currentUser;
        _jobScheduler = jobScheduler;
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

        if (auction.Type == "Auction" &&  oldAuction.EndDateTime != auction.EndDateTime && auction.EndDateTime != null)
        {
            if (!string.IsNullOrEmpty(auction.HangfireJobId))
            {
                _jobScheduler.CancelScheduledAuctionClosing(auction.HangfireJobId);
            }
            var newJobId = _jobScheduler.ScheduleAuctionClosing(auction.AuctionId, new DateTimeOffset(auction.EndDateTime));
            auction.HangfireJobId = newJobId;
            _unitOfWork.AuctionRepository.Update(auction);
            await _unitOfWork.SaveAsync();
        }


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
