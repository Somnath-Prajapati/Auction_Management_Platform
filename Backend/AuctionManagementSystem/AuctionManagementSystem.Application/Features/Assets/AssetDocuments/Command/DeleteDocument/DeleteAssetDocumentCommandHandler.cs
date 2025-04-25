using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetDocuments.Command.DeleteDocument
{
    public class DeleteAssetDocumentHandler : IRequestHandler<DeleteAssetDocumentCommand, bool>
    {
        private readonly IAssetDocumentRepository _repo;

        public DeleteAssetDocumentHandler(IAssetDocumentRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteAssetDocumentCommand request, CancellationToken cancellationToken)
        {
            return await _repo.DeleteAsync(request.Id);
        }
    }

}
