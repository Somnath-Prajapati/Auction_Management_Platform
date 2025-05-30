using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Exceptions;
using AuctionManagementSystem.Domain.Entities.Asset;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetWinner.Query.GetByUserIdAsync
{
    public class GetByUserIdAsyncQueryHandler : IRequestHandler<GetByUserIdAsyncQuery, List<AssetWinnerDto>>
    {
        private readonly IAssetWinnerRepository _assetWinnerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public GetByUserIdAsyncQueryHandler(IAssetWinnerRepository assetWinnerRepository, IMediator mediator, IMapper mapper, IUserRepository userRepository)
        {
            _assetWinnerRepository = assetWinnerRepository;
            _userRepository = userRepository;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<List<AssetWinnerDto>> Handle(GetByUserIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.userId);
            if (user == null)
            {
                throw new NotFoundException("User ID is Invalid:: No user Found");
            }
            var win = await _assetWinnerRepository.GetByUserIdAsync(request.userId);
            if (win == null) 
            {
                throw new NotFoundException("The User Has no Wins");
            }

            var winList = _mapper.Map<List<AssetWinnerDto>>(win);

            return winList;
        }
    }
}
