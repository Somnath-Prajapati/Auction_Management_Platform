using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AuctionManagementSystem.Application.Contracts.User
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderName);

    }
}
