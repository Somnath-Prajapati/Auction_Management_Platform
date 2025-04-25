using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetDocuments.Query.GetDocument
{
    public class GetAllAssetDocumentsHandler : IRequestHandler<GetAllAssetDocumentsQuery, IEnumerable<AssetDocumentDto>>
    {
        private readonly IAssetDocumentRepository _repo;
        private readonly IMapper _mapper;

        public GetAllAssetDocumentsHandler(IAssetDocumentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AssetDocumentDto>> Handle(GetAllAssetDocumentsQuery request, CancellationToken cancellationToken)
        {
            var documents = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<AssetDocumentDto>>(documents);
        }

        

        //public async Task<IEnumerable<TblAssetDocument>> Handle(GetAllAssetDocumentsQuery request, CancellationToken cancellationToken)
        //{
        //    //var result = await _repo.GetAllAsync();
        //    //return _mapper.Map<IEnumerable<AssetDocumentDto>>(result);
        //    return await _repo.GetAllAsync();

        //}





    }

}
