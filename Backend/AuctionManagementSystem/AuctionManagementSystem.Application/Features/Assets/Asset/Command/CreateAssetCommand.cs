using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Command
{
    public class CreateAssetCommand : IRequest<int>
    {
        public CreateAssetsDto Dto { get; set; }
        public List<IFormFile> GalleryFiles { get; set; }
        public List<IFormFile> DocumentFiles { get; set; }
    }

}
