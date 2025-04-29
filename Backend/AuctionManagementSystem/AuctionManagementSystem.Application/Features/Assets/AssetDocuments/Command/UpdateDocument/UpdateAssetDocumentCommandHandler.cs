using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Features.Assets.Asset.Command.UpdateAsset;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetDocuments.Command.UpdateDocument
{
    public class UpdateAssetDocumentHandler : IRequestHandler<UpdateAssetDocumentCommand, bool>
    {
        private readonly IAssetDocumentRepository _repository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public UpdateAssetDocumentHandler(IAssetDocumentRepository repository, IFileService fileService, IMapper mapper)
        {
            _repository = repository;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateAssetDocumentCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.Id);
            if (existing == null) return false;

            // Check if the document type is valid
            string[] allowedDocumentTypes = { "pdf", "docx", "xlsx", "jpg", "jpeg", "png" };
            if (request.Dto.DocumentType != null && !allowedDocumentTypes.Contains(request.Dto.DocumentType.ToLower()))
            {
                throw new ArgumentException("Invalid document type. Allowed types are: pdf, docx, xlsx, jpg, jpeg, png");
            }

            if (request.Dto.File != null)
            {
                // File size validation
                if (request.Dto.File.Length > 1 * 1024 * 1024) // 1MB limit
                    throw new ArgumentException("File size should not exceed 1 MB");

                // File type validation
                string[] allowedExtensions = { ".pdf", ".docx", ".xlsx", ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(request.Dto.File.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                    throw new ArgumentException("Invalid file type. Allowed types are: .pdf, .docx, .xlsx, .jpg, .jpeg, .png");

                // Save the new file and update file path
                var newPath = await _fileService.SaveFileAsync(request.Dto.File, "AssetDocuments");

                // Delete the old file if it exists
                if (!string.IsNullOrEmpty(existing.FilePath))
                {
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existing.FilePath.Replace("/", "\\"));
                    if (File.Exists(fullPath))
                        File.Delete(fullPath);
                }

                existing.FilePath = newPath; // Update the file path
            }

            // Update the other fields (AssetId and DocumentType)
            existing.AssetId = request.Dto.AssetId;
            existing.DocumentType = request.Dto.DocumentType;

            return await _repository.UpdateAsync(request.Id, existing); // Save changes to DB
        }
    }


}
