using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Domain.Entities.User;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Query.GetSellers
{
    public class GetSellersQueryHandler : IRequestHandler<GetSellersQuery, IEnumerable<SellerDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAssetsRepository _assetsRepository;

        public GetSellersQueryHandler(IAssetsRepository assetsRepository, IMapper mapper)
        {
            _assetsRepository = assetsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SellerDto>> Handle(GetSellersQuery request, CancellationToken cancellationToken)
        {
            var sellers = await _assetsRepository.getAllSeller();

            return _mapper.Map<IEnumerable<SellerDto>>(sellers);

        }
    }
}
