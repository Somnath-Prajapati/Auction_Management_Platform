using AuctionManagementSystem.Application.Contracts.User;

namespace AuctionManagementSystem.Api.Services
{
    public class FileService : IFileService
    {
        public async Task<string> SaveFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file");

            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(rootPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine(folderName, uniqueFileName).Replace("\\", "/");
        }

        public Task DeleteFileAsync(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
                return Task.CompletedTask;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath.Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Task.CompletedTask;
        }
    }
}
