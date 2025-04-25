using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class AssetDocumentUploadDto
    {
        public int? AssetId { get; set; }
        public string? DocumentType { get; set; }
        public IFormFile? File { get; set; }
    }

}
