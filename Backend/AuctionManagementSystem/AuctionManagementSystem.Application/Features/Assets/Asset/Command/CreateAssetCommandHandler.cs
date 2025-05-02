using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Domain.Entities.Asset;
using AutoMapper;
using MediatR;


namespace AuctionManagementSystem.Application.Features.Assets.Asset.Command
{
    public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, int>
    {
        private readonly IAssetsRepository _repo;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public CreateAssetCommandHandler(IAssetsRepository repo, IFileService fileService, IMapper mapper)
        {
            _repo = repo;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = _mapper.Map<TblAsset>(request.Dto);
            asset.Details = JsonSerializer.Serialize(request.Dto.Details); // Save details JSON

            var galleries = new List<TblAssetGallery>();
            for (int i = 0; i < request.GalleryFiles.Count; i++)
            {
                var file = request.GalleryFiles[i];
                var path = await _fileService.SaveFileAsync(file, "AssetGallery");

                galleries.Add(new TblAssetGallery
                {
                    FilePath = path,
                    SortOrder = request.Dto.Galleries[i].SortOrder,
                    MediaType = request.Dto.Galleries[i].MediaType
                });
            }

            var documents = new List<TblAssetDocument>();
            for (int i = 0; i < request.DocumentFiles.Count; i++)
            {
                var file = request.DocumentFiles[i];
                var path = await _fileService.SaveFileAsync(file, "AssetDocuments");

                documents.Add(new TblAssetDocument
                {
                    FilePath = path,
                    DocumentType = request.Dto.Documents[i].DocumentType
                });
            }

            return await _repo.AddAssetWithMediaAsync(asset, galleries, documents);
        }
    }

}
