using AuctionManagementSystem.Application.Contracts.User;

namespace AuctionManagementSystem.Api.Services
{
    public class FileService : IFileService
    {
        public async Task<string> SaveFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file");

            // Get current directory (API root) and combine with folder name
            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(rootPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return relative path (e.g., for storing in DB)
            return Path.Combine(folderName, uniqueFileName).Replace("\\", "/");
        }
    }

}
