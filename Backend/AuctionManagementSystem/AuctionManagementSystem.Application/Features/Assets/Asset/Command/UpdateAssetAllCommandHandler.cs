using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Features.Assets.AssetDetails.Command;
using AuctionManagementSystem.Application.Features.Assets.AssetDocuments.Command.AddDocument;
using AuctionManagementSystem.Application.Features.Assets.AssetGallery.Command.AddAssetGallery;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Auction;
using MediatR;
using Newtonsoft.Json;

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Command
{
    public class UpdateAssetAllCommandHandler : IRequestHandler<UpdateAssetAllCommand,bool>
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IAssetGalleryRepository _galleryRepo;
        private readonly IAssetDocumentRepository _documentRepo;
        private readonly IAuctionAssetRepository _auctionAssetRepo;
        private readonly IFileService _fileService;
        private readonly IAssetDetailRepository _assetDetailRepository;
        private readonly IMediator _mediator;
        public UpdateAssetAllCommandHandler(
                IAssetsRepository assetsRepository,
                IAssetGalleryRepository galleryRepo,
                IAssetDocumentRepository documentRepo,
                IAuctionAssetRepository auctionAssetRepo,
                IFileService fileService,
                IAssetDetailRepository assetDetailRepository
,
                IMediator mediator
            )
        {
            _assetsRepository = assetsRepository;
            _galleryRepo = galleryRepo;
            _documentRepo = documentRepo;
            _auctionAssetRepo = auctionAssetRepo;
            _fileService = fileService;
            _assetDetailRepository = assetDetailRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(UpdateAssetAllCommand request, CancellationToken cancellationToken)
        {
            var dto = request.dto;
            var asset = await _assetsRepository.GetIdDeleteAsync(dto.AssetId);
            if (asset == null) return false;


            List<UpdateAssetDetailDto> details;
            try
            {
                details = JsonConvert.DeserializeObject<List<UpdateAssetDetailDto>>(dto.DetailsJson ?? "[]");
                if (details == null) details = new List<UpdateAssetDetailDto>();
            }
            catch
            {
                throw new Exception("Invalid details format.");

            }


            // Update main asset fields --------------------

            asset.AssetNumber = dto.AssetNumber;
            asset.Title = dto.Title;
            asset.CategoryId = dto.CategoryId;
            asset.Deposit = dto.Deposit;
            asset.SellerId = dto.SellerId;
            asset.Commission = dto.Commission;
            asset.StartingPrice = dto.StartingPrice;
            asset.ReserveAmount = dto.ReserveAmount;
            asset.IncrementalTime = dto.IncrementalTime;
            asset.MinIncrement = dto.MinIncrement;
            asset.MakeOffer = dto.MakeOffer;
            asset.Featured = dto.Featured;
            asset.AwardingId = dto.AwardingId;
            asset.StatusId = dto.StatusId;
            asset.Vatid = dto.Vatid;
            asset.Vatpercent = dto.Vatpercent;
            asset.CourtCaseNumber = dto.CourtCaseNumber;
            asset.RegistrationDeadline = dto.RegistrationDeadline;
            asset.Description = dto.Description;
            asset.MapLatitude = dto.MapLatitude;
            asset.MapLongitude = dto.MapLongitude;
            asset.AdminFees = dto.AdminFees;
            asset.AuctionFees = dto.AuctionFees;
            asset.BuyerCommission = dto.BuyerCommission;
            asset.WinnerId = dto.WinnerId;
            //asset.awa = dto.AwardedPrice;
            asset.SalesNotes = dto.SalesNotes;
            asset.RequestForInquiry = dto.RequestForInquiry;
            asset.RequestForViewing = dto.RequestForViewing;
            asset.UpdatedAt = DateTime.UtcNow;




            //await _assetDetailRepository.UpdateDetailsAsync(dto.AssetId, dto.Attributes);


            if (details != null && details.Any())
            {
                await _assetDetailRepository.RemoveAssetDetailsAsync(dto.AssetId);
                
                foreach (var detail in details)
                {
                    var assetDetail = new TblAssetDetail
                    {
                        AssetId = dto.AssetId,
                        AttributeName = detail.AttributeName,
                        AttributeValue = detail.AttributeValue
                    };

                    await _mediator.Send(new AddAssetDetailCommand(assetDetail));
                }
            }

            // Save changes to asset details in TblAssetDetails
            //await _assetDetailRepository.SaveChangesAsync();



            //var existingGalleryImages = await _galleryRepo.GetByAssetIdAsync(request.dto.AssetId);
            //foreach (var oldImage in existingGalleryImages)
            //{
            //    if (!string.IsNullOrEmpty(oldImage.FilePath))
            //    {
            //        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", oldImage.FilePath.Replace("/", "\\"));
            //        if (File.Exists(fullPath))
            //            File.Delete(fullPath);
            //    }
            //    await _galleryRepo.DeleteAsync(oldImage.GalleryId); 
            //}

            //if (dto.NewGalleryImages != null && dto.NewGalleryImages.Any())
            //{
            //    foreach (var file in dto.NewGalleryImages)
            //    {
            //        var galleryDto = new AssetsGalleryDto
            //        {
            //            AssetId = dto.AssetId,
            //            File = file,
            //            MediaType = "image",
            //            SortOrder = 0
            //        };

            //        await _mediator.Send(new AddAssetGalleryCommand(galleryDto));
            //    }
            //}

            if (dto.NewGalleryImages != null)
            {
                var existingGalleryImages = await _galleryRepo.GetByAssetIdAsync(request.dto.AssetId);
                
                foreach (var oldImage in existingGalleryImages)
                {
                    if (!string.IsNullOrEmpty(oldImage.FilePath))
                    {
                        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", oldImage.FilePath.Replace("/", "\\"));
                        if (File.Exists(fullPath))
                            File.Delete(fullPath);
                    }
                    await _galleryRepo.DeleteAsync(oldImage.GalleryId);
                }

                if (dto.NewGalleryImages.Any())
                {
                    foreach (var file in dto.NewGalleryImages)
                    {
                        var galleryDto = new AssetsGalleryDto
                        {
                            AssetId = dto.AssetId,
                            File = file,
                            MediaType = "image",
                            SortOrder = 0
                        };

                        await _mediator.Send(new AddAssetGalleryCommand(galleryDto));
                    }
                }
            }



            if (dto.NewDocuments != null && dto.NewDocuments.Any())
            {
                await _documentRepo.DeleteDocumentsByAssetIdAsync(dto.AssetId);
                
                foreach (var document in dto.NewDocuments)
                {
                    var assetDocument = new TblAssetDocument
                    {
                        AssetId = dto.AssetId,
                        DocumentType="pdf",
                        FilePath = await _fileService.SaveFileAsync(document, "AssetDocuments") 
                    };

                    await _documentRepo.AddAsync(assetDocument);
                }
            }

            await _assetsRepository.UpdateAsync(asset);


            return true;    
        }
    }
}
