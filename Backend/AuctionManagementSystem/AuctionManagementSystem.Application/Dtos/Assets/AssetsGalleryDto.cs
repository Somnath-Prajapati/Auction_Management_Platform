using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class AssetsGalleryDto
    {
        public int? AssetId { get; set; }
        public string? MediaType { get; set; }
        public IFormFile? File { get; set; }

        public string? ImageUrl { get; set; }
        public int? SortOrder { get; set; }
    }
}
