
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Domain.Entities.Asset;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetGallery.Command.UpdateAssetGallery
{
    public class UpdateAssetGalleryHandler : IRequestHandler<UpdateAssetGalleryCommand, bool>
    {
        private readonly IAssetGalleryRepository _repository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public UpdateAssetGalleryHandler(IAssetGalleryRepository repository, IFileService fileService, IMapper mapper)
        {
            _repository = repository;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateAssetGalleryCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.Id);
            if (existing == null) return false;

            if (request.Dto.File != null)
            {
                if (request.Dto.File.Length > 1 * 1024 * 1024)
                    throw new ArgumentException("File size should not exceed 1 MB");

                string[] allowedExtensions = [".jpg", ".jpeg", ".png"];
                var extension = Path.GetExtension(request.Dto.File.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                    throw new ArgumentException("Invalid file type. Allowed types are: .jpg, .jpeg, .png");

                // Save new file
                var newPath = await _fileService.SaveFileAsync(request.Dto.File, "AssetGallery");

                // Delete old file
                if (!string.IsNullOrEmpty(existing.FilePath))
                {
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existing.FilePath.Replace("/", "\\"));
                    if (File.Exists(fullPath))
                        File.Delete(fullPath);
                }

                existing.FilePath = newPath;
            }

            existing.AssetId = request.Dto.AssetId;
            existing.SortOrder = request.Dto.SortOrder;
            existing.MediaType = request.Dto.MediaType;

            return await _repository.UpdateAsync(request.Id, existing);
        }
    }
}

        //private readonly IAssetGalleryRepository _repository;
        //private readonly IFileService _fileService;
        //private readonly IMapper _mapper;

        //public UpdateAssetGalleryHandler(IAssetGalleryRepository repository, IFileService fileService, IMapper mapper)
        //{
        //    _repository = repository;
        //    _fileService = fileService;
        //    _mapper = mapper;
        //}

        //public async Task<bool> Handle(UpdateAssetGalleryCommand request, CancellationToken cancellationToken)
        //{
        //    var existing = await _repository.GetByIdAsync(request.Id);
        //    if (existing == null) return false;

        //    // Apply non-null properties from DTO to existing entity
        //    _mapper.Map(request.Dto, existing);

        //    // Handle file separately (DTO doesn't carry FilePath anymore)
        //    if (request.Dto.File != null)
        //    {
        //        existing.FilePath = await _fileService.SaveFileAsync(request.Dto.File, "AssetGallery");
        //    }

        //    return await _repository.UpdateAsync(request.Id, existing);
        //}