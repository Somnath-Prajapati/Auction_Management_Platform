using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AuctionManagementSystem.Application.Contracts.Assets
{
    public interface IFileServiceGallery
    {
        Task<string> SaveFileAsync(IFormFile file, string folderName);
    }
}
