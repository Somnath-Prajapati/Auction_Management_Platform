using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class AssetDocumentDto
    {
        public int DocumentId { get; set; }
        public int? AssetId { get; set; }
        public string? DocumentType { get; set; }
        public string? FilePath { get; set; }  
    }

}
