using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.Asset;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetDocuments.Query.GetDocumentById
{
    public record GetAssetDocumentByIdQuery(int Id) : IRequest<TblAssetDocument?>;
}
