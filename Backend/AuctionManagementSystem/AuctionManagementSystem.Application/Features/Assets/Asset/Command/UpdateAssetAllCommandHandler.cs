using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Features.Assets.AssetDetails.Command;
using AuctionManagementSystem.Application.Features.Assets.AssetDocuments.Command.AddDocument;
using AuctionManagementSystem.Application.Features.Assets.AssetGallery.Command.AddAssetGallery;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Auction;
using AuctionManagementSystem.Domain.Entities.Translations;
using MediatR;
using Newtonsoft.Json;

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Command
{
    public class UpdateAssetAllCommandHandler : IRequestHandler<UpdateAssetAllCommand, bool>
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IAssetGalleryRepository _galleryRepo;
        private readonly IAssetDocumentRepository _documentRepo;
        private readonly IAuctionAssetRepository _auctionAssetRepo;
        private readonly IFileService _fileService;
        private readonly IAssetDetailRepository _assetDetailRepository;
        private readonly IMediator _mediator;
        private readonly IAuditTrailService _auditTrailService;
        private readonly ICurrentUserService _currentUser;

        public UpdateAssetAllCommandHandler(
            IAssetsRepository assetsRepository,
            IAssetGalleryRepository galleryRepo,
            IAssetDocumentRepository documentRepo,
            IAuctionAssetRepository auctionAssetRepo,
            IFileService fileService,
            IAssetDetailRepository assetDetailRepository,
            IMediator mediator,
            IAuditTrailService auditTrailService,
            ICurrentUserService currentUser)
        {
            _assetsRepository = assetsRepository;
            _galleryRepo = galleryRepo;
            _documentRepo = documentRepo;
            _auctionAssetRepo = auctionAssetRepo;
            _fileService = fileService;
            _assetDetailRepository = assetDetailRepository;
            _mediator = mediator;
            _auditTrailService = auditTrailService;
            _currentUser = currentUser;
        }

        public async Task<bool> Handle(UpdateAssetAllCommand request, CancellationToken cancellationToken)
        {
            var dto = request.dto;
            var asset = await _assetsRepository.GetIdDeleteAsync(dto.AssetId);
            if (asset == null) return false;

            // Capture BEFORE state for audit logging
            var beforeChange = JsonConvert.SerializeObject(asset, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            // Deserialize details
            List<UpdateAssetDetailDto> details;
            try
            {
                details = JsonConvert.DeserializeObject<List<UpdateAssetDetailDto>>(dto.DetailsJson ?? "[]") ?? new List<UpdateAssetDetailDto>();
            }
            catch
            {
                throw new Exception("Invalid details format.");
            }

            // Update asset fields
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
            asset.SalesNotes = dto.SalesNotes;
            asset.RequestForInquiry = dto.RequestForInquiry;
            asset.RequestForViewing = dto.RequestForViewing;
            asset.UpdatedAt = DateTime.UtcNow;


            if (dto.LanguageId == 2)
            {
                var translation = await _assetsRepository.GetAssetTranslationByAssetIdAsync(dto.AssetId);

                if (translation != null)
                {
                    translation.Title = dto.TranslatedTitle;
                    translation.Description = dto.TranslatedDescription;
                    translation.SalesNotes = dto.TranslatedSalesNotes;
                    translation.CreatedAt = DateTime.UtcNow;

                    await _assetsRepository.UpdateAssetTranslationAsync(translation);
                }
                else
                {
                    var newTranslation = new tblAssetTranslation
                    {
                        AssetId = dto.AssetId,
                        Title = dto.TranslatedTitle,
                        Description = dto.TranslatedDescription,
                        SalesNotes = dto.TranslatedSalesNotes,
                        LanguageId = 2, // Arabic
                        CreatedAt = DateTime.UtcNow,
                    };

                    await _assetsRepository.AddAssetTranslationAsync(newTranslation);
                }
            }
            else if (dto.LanguageId == 0 && dto.RemoveTranslation == true)
            {
                var translation = await _assetsRepository.GetAssetTranslationByAssetIdAsync(dto.AssetId);
                if (translation != null)
                {
                    await _assetsRepository.DeleteAssetTranslationAsync(translation);
                }
            }



            // Update details
            if (details.Any())
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

            // Add new gallery images
            if (dto.NewGalleryImages != null)
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

            // Add new documents
            if (dto.NewDocuments != null && dto.NewDocuments.Any())
            {
                foreach (var document in dto.NewDocuments)
                {
                    var assetDocument = new TblAssetDocument
                    {
                        AssetId = dto.AssetId,
                        DocumentType = "pdf",
                        FilePath = await _fileService.SaveFileAsync(document, "AssetDocuments")
                    };

                    await _documentRepo.AddAsync(assetDocument);
                }
            }

            // Save to DB
            await _assetsRepository.UpdateAsync(asset);

            // Capture AFTER state for audit logging
            var afterChange = JsonConvert.SerializeObject(asset, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            // Log audit
            try
            {
                await _auditTrailService.LogChangeAsync(
                    userId: _currentUser.UserId,
                    username: _currentUser.Username,
                    roleName: _currentUser.RoleName,
                    modelName: "Asset",
                    changeType: "Update",
                    recordId: asset.AssetId,
                    beforeChange: beforeChange,
                    afterChange: afterChange
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine("Audit Trail Logging Failed: " + ex.Message);
            }

            Console.WriteLine($"Asset All Updated By: {_currentUser.Username} ({_currentUser.UserId})");

            return true;
        }
    }

}
