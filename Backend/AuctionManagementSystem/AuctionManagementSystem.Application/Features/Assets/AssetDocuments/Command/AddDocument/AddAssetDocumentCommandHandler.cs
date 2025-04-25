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

namespace AuctionManagementSystem.Application.Features.Assets.AssetDocuments.Command.AddDocument
{
    public class AddAssetDocumentHandler : IRequestHandler<AddAssetDocumentCommand, int>
    {
        private readonly IAssetDocumentRepository _repo;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public AddAssetDocumentHandler(IAssetDocumentRepository repo, IMapper mapper, IFileService fileService)
        {
            _repo = repo;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<int> Handle(AddAssetDocumentCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TblAssetDocument>(request.Dto);

            if (request.Dto.File != null)
            {
                if (request.Dto.File.Length > 1 * 1024 * 1024)
                    throw new ArgumentException("File size should not exceed 1 MB");

                string[] allowedExtensions = [".pdf", ".docx", ".xlsx", ".jpg", ".jpeg", ".png"];
                var extension = Path.GetExtension(request.Dto.File.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                    throw new ArgumentException("Invalid file type. Allowed types are: .pdf, .docx, .xlsx, .jpg, .jpeg, .png");

                entity.FilePath = await _fileService.SaveFileAsync(request.Dto.File, "AssetDocuments");
            }

            return await _repo.AddAsync(entity);
        }
    }

}
