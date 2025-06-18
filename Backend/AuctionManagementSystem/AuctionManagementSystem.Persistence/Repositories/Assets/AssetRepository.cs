using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Dtos.GetFeaturedAssets;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Translations;
using AuctionManagementSystem.Domain.Entities.User;
using AuctionManagementSystem.Persistence.Context;
using EventStore.ClientAPI;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AuctionManagementSystem.Persistence.Repositories.Assets
{
    public class AssetRepository : IAssetsRepository
    {
        private readonly AuctionManagementDbContext _context;
        private readonly string _connectionString;

        public AssetRepository(AuctionManagementDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<IEnumerable<GetAssetsFormDto>> GetAllAsync()
        {
                   var assets = await _context.TblAssets
                //.Where(a => a.IsActive)
                //.Where(a => a.IsActive)
                .Where(a => a.IsDeleted == false)
                .OrderByDescending(a => a.UpdatedAt ?? a.CreatedAt)
                .Include(a => a.Category)
                .Include(a => a.Status)
                .Include(a => a.Vat)
                .Include(a => a.Awarding)
                .Include(a => a.Winner)
                .Include(a => a.TblAssetGalleries)
                .Include(a => a.TblAssetDocuments)
                .Include(a => a.TblAuctionAssets)
                .ThenInclude(aa => aa.Auction)
                .Select(a => new GetAssetsFormDto
                {
                    AssetId = a.AssetId,
                    Title = a.Title,
                    CategoryId = a.CategoryId,
                    CategoryName = a.Category != null ? a.Category.CategoryName : null,
                    Deposit = a.Deposit,
                    SellerId = a.SellerId,
                    Commission = a.Commission,
                    StartingPrice = a.StartingPrice,
                    ReserveAmount = a.ReserveAmount,
                    IncrementalTime = a.IncrementalTime,
                    MinIncrement = a.MinIncrement,
                    MakeOffer = a.MakeOffer,
                    Featured = a.Featured,
                    AwardingId = a.AwardingId,
                    AwardingMethod = a.Awarding != null ? a.Awarding.AwardingMethod : null,
                    StatusId = a.StatusId,
                    StatusName = a.Status != null ? a.Status.StatusName : null,
                    Vatid = a.Vatid,
                    VatType = a.Vat != null ? a.Vat.Vattype : null,
                    Vatpercent = a.Vatpercent,
                    CourtCaseNumber = a.CourtCaseNumber,
                    RegistrationDeadline = a.RegistrationDeadline,
                    Description = a.Description,
                    MapLatitude = a.MapLatitude,
                    MapLongitude = a.MapLongitude,
                    AdminFees = a.AdminFees,
                    AuctionFees = a.AuctionFees,
                    BuyerCommission = a.BuyerCommission,
                    RequestForViewing=a.RequestForViewing,
                    RequestForInquiry=a.RequestForInquiry,
                    WinnerId = a.WinnerId,
                    WinnerName = a.Winner != null ? a.Winner.User.Name : null,
                    AwardedPrice = a.Winner != null ? a.Winner.AwardedPrice : null,
                    SalesNotes = a.SalesNotes,
                    AssetNumber = a.AssetNumber,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt,
                    isDeleted = a.IsDeleted,
                    IsAvailableForDirectSale = a.IsAvailableForDirectSale,
                    AuctionStatusId = a.TblAuctionAssets.Select(aa => aa.Auction.StatusId).FirstOrDefault(),

                    Galleries = a.TblAssetGalleries.Select(g => new AssetGalleryDtos
                    {
                        GalleryId = g.GalleryId,
                        MediaType = g.MediaType,
                        FilePath = g.FilePath,
                        SortOrder = g.SortOrder
                    }).ToList(),
                    Documents = a.TblAssetDocuments.Select(d => new AssetDocumentFormDto
                    {
                        DocumentId = d.DocumentId,

                        DocumentType = d.DocumentType,
                        FilePath = d.FilePath
                    }).ToList(),

                    Attributes = a.TblAssetDetails.Select(d => new AssetDetailDtoo
                    {
                        AttributeName = d.AttributeName,
                        AttributeValue = d.AttributeValue
                    }).ToList(),

                })
                .ToListAsync();

                return assets;
        }

        public async Task<GetAssetsFormDto> GetByIdAsync(int id)
        {
        var asset = await _context.TblAssets
          .Where(a => a.IsDeleted == false)
         .Include(a => a.Category)
         .Include(a => a.Status)
         .Include(a => a.Seller)
         .Include(a => a.Awarding)
         .Include(a => a.Vat)
         .Include(a => a.TblAssetGalleries)
         .Include(a => a.TblAssetDocuments)
         .Include(a => a.TblAssetDetails)
         .Include(a => a.TblAuctionAssets)
         .ThenInclude(aa => aa.Auction)
         .Where(a => a.AssetId == id)
         .Select(a => new GetAssetsFormDto
         {
             AssetId = a.AssetId,
             Title = a.Title,
             CategoryId = a.CategoryId,
             CategoryName = a.Category != null ? a.Category.CategoryName : null,
             Deposit = a.Deposit,
             SellerId = a.SellerId,
             Commission = a.Commission,
             StartingPrice = a.StartingPrice,
             ReserveAmount = a.ReserveAmount,
             IncrementalTime = a.IncrementalTime,
             MinIncrement = a.MinIncrement,
             MakeOffer = a.MakeOffer,
             Featured = a.Featured,
             AwardingId = a.AwardingId,
             AwardingMethod = a.Awarding != null ? a.Awarding.AwardingMethod : null,
             StatusId = a.StatusId,
             StatusName = a.Status != null ? a.Status.StatusName : null,
             Vatid = a.Vatid,
             VatType = a.Vat != null ? a.Vat.Vattype : null,
             Vatpercent = a.Vatpercent,
             CourtCaseNumber = a.CourtCaseNumber,
             RegistrationDeadline = a.RegistrationDeadline,
             Description = a.Description,
             MapLatitude = a.MapLatitude,
             MapLongitude = a.MapLongitude,
             AdminFees = a.AdminFees,
             AuctionFees = a.AuctionFees,
             BuyerCommission = a.BuyerCommission,
             WinnerId = a.WinnerId,
             WinnerName = a.Winner != null && a.Winner.User != null ? a.Winner.User.Name : null,
             AwardedPrice = a.Winner != null ? a.Winner.AwardedPrice : null,
             SalesNotes = a.SalesNotes,
             AssetNumber = a.AssetNumber,
             CreatedAt = a.CreatedAt,
             UpdatedAt = a.UpdatedAt,
             RequestForInquiry=a.RequestForInquiry,
             RequestForViewing = a.RequestForViewing,
             IsAvailableForDirectSale = a.IsAvailableForDirectSale,
             isDeleted = a.IsDeleted,
             AuctionStatusId = a.TblAuctionAssets.Select(aa => aa.Auction.StatusId).FirstOrDefault(),

             // for auctionids 
             AuctionIds = a.TblAuctionAssets
                .Select(aa => aa.AuctionId)
                .ToList(),
             Galleries = a.TblAssetGalleries.Select(g => new AssetGalleryDtos
             {
                 GalleryId = g.GalleryId,
                 MediaType = g.MediaType,
                 FilePath = g.FilePath,
                 SortOrder = g.SortOrder
             }).ToList(),
             Documents = a.TblAssetDocuments.Select(d => new AssetDocumentFormDto
             {
                 DocumentId = d.DocumentId,
                 DocumentType = d.DocumentType,
                 FilePath = d.FilePath
             }).ToList(),
             // Add the asset details (attributes) here
             Attributes = a.TblAssetDetails.Select(d => new AssetDetailDtoo
             {
                 AttributeName = d.AttributeName,
                 AttributeValue = d.AttributeValue
             }).ToList()
         })
         .FirstOrDefaultAsync();

            return asset;
        }



        public async Task<GetAssetsFormDto> GetByIdViewAsync(int id, string languageCode)
        {
            // Step 1: Get LanguageId from Code
            var language = await _context.TblLanguages
                .Where(l => l.Code == languageCode && l.IsActive)
                .FirstOrDefaultAsync();

            if (language == null)
            {
                throw new Exception("Invalid or inactive language code.");
            }

            int languageId = language.LanguageId;

            // Step 2: Fetch the asset
            var asset = await _context.TblAssets
                .Include(a => a.Category)
                .Include(a => a.Status)
                .Include(a => a.Seller)
                .Include(a => a.Awarding)
                .Include(a => a.Vat)
                .Include(a => a.Winner)
                    .ThenInclude(w => w.User)
                .Include(a => a.TblAssetGalleries)
                .Include(a => a.TblAssetDocuments)
                .Include(a => a.TblAssetDetails)
                .Include(a => a.TblAuctionAssets)
                    .ThenInclude(aa => aa.Auction)
                .Where(a => a.AssetId == id)
                .Select(a => new GetAssetsFormDto
                {
                    AssetId = a.AssetId,
                    Title = a.Title,
                    CategoryId = a.CategoryId,
                    CategoryName = a.Category != null ? a.Category.CategoryName : null,
                    Deposit = a.Deposit,
                    SellerId = a.SellerId,
                    Commission = a.Commission,
                    StartingPrice = a.StartingPrice,
                    ReserveAmount = a.ReserveAmount,
                    IncrementalTime = a.IncrementalTime,
                    MinIncrement = a.MinIncrement,
                    MakeOffer = a.MakeOffer,
                    Featured = a.Featured,
                    AwardingId = a.AwardingId,
                    AwardingMethod = a.Awarding != null ? a.Awarding.AwardingMethod : null,
                    StatusId = a.StatusId,
                    StatusName = a.Status != null ? a.Status.StatusName : null,
                    Vatid = a.Vatid,
                    VatType = a.Vat != null ? a.Vat.Vattype : null,
                    Vatpercent = a.Vatpercent,
                    CourtCaseNumber = a.CourtCaseNumber,
                    RegistrationDeadline = a.RegistrationDeadline,
                    Description = a.Description,
                    MapLatitude = a.MapLatitude,
                    MapLongitude = a.MapLongitude,
                    AdminFees = a.AdminFees,
                    AuctionFees = a.AuctionFees,
                    BuyerCommission = a.BuyerCommission,
                    WinnerId = a.WinnerId,
                    WinnerName = a.Winner != null && a.Winner.User != null ? a.Winner.User.Name : null,
                    AwardedPrice = a.Winner != null ? a.Winner.AwardedPrice : null,
                    SalesNotes = a.SalesNotes,
                    AssetNumber = a.AssetNumber,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt,
                    RequestForInquiry = a.RequestForInquiry,
                    RequestForViewing = a.RequestForViewing,
                    IsAvailableForDirectSale = a.IsAvailableForDirectSale,
                    isDeleted = a.IsDeleted,
                    AuctionStatusId = a.TblAuctionAssets.Select(aa => aa.Auction.StatusId).FirstOrDefault(),
                    AuctionIds = a.TblAuctionAssets.Select(aa => aa.AuctionId).ToList(),
                    Galleries = a.TblAssetGalleries.Select(g => new AssetGalleryDtos
                    {
                        GalleryId = g.GalleryId,
                        MediaType = g.MediaType,
                        FilePath = g.FilePath,
                        SortOrder = g.SortOrder
                    }).ToList(),
                    Documents = a.TblAssetDocuments.Select(d => new AssetDocumentFormDto
                    {
                        DocumentId = d.DocumentId,
                        DocumentType = d.DocumentType,
                        FilePath = d.FilePath
                    }).ToList(),
                    Attributes = a.TblAssetDetails.Select(d => new AssetDetailDtoo
                    {
                        AttributeName = d.AttributeName,
                        AttributeValue = d.AttributeValue
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (asset == null) return null;

            // Step 3: Override with translated values
            var translation = await _context.TblAssetTranslations
                .Where(t => t.AssetId == id && t.LanguageId == languageId)
                .FirstOrDefaultAsync();

            if (translation != null)
            {
                asset.Title = translation.Title ?? asset.Title;
                asset.Description = translation.Description ?? asset.Description;
                asset.SalesNotes = translation.SalesNotes ?? asset.SalesNotes;
            }

            return asset;
        }


        public async Task<List<GetAssetsFormDto>> GetDirectAllAsync(Expression<Func<TblAsset, bool>> predicate,string languageCode)
        {
            var language = await _context.TblLanguages
              .Where(l => l.Code == languageCode && l.IsActive)
              .FirstOrDefaultAsync();

            if (language == null)
            {
                throw new Exception("Invalid or inactive language code.");
            }

            int languageId = language.LanguageId;

            var assets = await _context.TblAssets
     .Where(predicate)
     .Where(a => !a.IsDeleted)
     .Include(a => a.Category)
     .Include(a => a.Status)
     .Include(a => a.Awarding)
     .Include(a => a.Vat)
     .Include(a => a.Winner).ThenInclude(w => w.User)
     .Include(a => a.TblAssetGalleries)
     .Include(a => a.TblAssetDocuments)
     .Include(a => a.TblAssetDetails)
     .Include(a => a.TblAuctionAssets).ThenInclude(aa => aa.Auction)
     .GroupJoin(
         _context.TblAssetTranslations.Where(t => t.LanguageId == languageId),
         a => a.AssetId,
         t => t.AssetId,
         (a, translations) => new { Asset = a, Translation = translations.FirstOrDefault() }
     )
     .Select(x => new GetAssetsFormDto
     {
         AssetId = x.Asset.AssetId,
         Title = x.Translation.Title ?? x.Asset.Title,
         Description = x.Translation.Description ?? x.Asset.Description,
         SalesNotes = x.Translation.SalesNotes ?? x.Asset.SalesNotes,

         CategoryId = x.Asset.CategoryId,
         CategoryName = x.Asset.Category != null ? x.Asset.Category.CategoryName : null,
         Deposit = x.Asset.Deposit,
         SellerId = x.Asset.SellerId,
         Commission = x.Asset.Commission,
         StartingPrice = x.Asset.StartingPrice,
         ReserveAmount = x.Asset.ReserveAmount,
         IncrementalTime = x.Asset.IncrementalTime,
         MinIncrement = x.Asset.MinIncrement,
         MakeOffer = x.Asset.MakeOffer,
         Featured = x.Asset.Featured,
         AwardingId = x.Asset.AwardingId,
         AwardingMethod = x.Asset.Awarding != null ? x.Asset.Awarding.AwardingMethod : null,
         StatusId = x.Asset.StatusId,
         StatusName = x.Asset.Status != null ? x.Asset.Status.StatusName : null,
         Vatid = x.Asset.Vatid,
         VatType = x.Asset.Vat != null ? x.Asset.Vat.Vattype : null,
         Vatpercent = x.Asset.Vatpercent,
         CourtCaseNumber = x.Asset.CourtCaseNumber,
         RegistrationDeadline = x.Asset.RegistrationDeadline,
         MapLatitude = x.Asset.MapLatitude,
         MapLongitude = x.Asset.MapLongitude,
         AdminFees = x.Asset.AdminFees,
         AuctionFees = x.Asset.AuctionFees,
         BuyerCommission = x.Asset.BuyerCommission,
         RequestForViewing = x.Asset.RequestForViewing,
         RequestForInquiry = x.Asset.RequestForInquiry,
         WinnerId = x.Asset.WinnerId,
         WinnerName = x.Asset.Winner != null ? x.Asset.Winner.User.Name : null,
         AwardedPrice = x.Asset.Winner != null ? x.Asset.Winner.AwardedPrice : null,
         AssetNumber = x.Asset.AssetNumber,
         CreatedAt = x.Asset.CreatedAt,
         UpdatedAt = x.Asset.UpdatedAt,
         AuctionStatusId = x.Asset.TblAuctionAssets.Select(aa => aa.Auction.StatusId).FirstOrDefault(),
         IsAvailableForDirectSale = x.Asset.IsAvailableForDirectSale,
         Galleries = x.Asset.TblAssetGalleries.Select(g => new AssetGalleryDtos
         {
             GalleryId = g.GalleryId,
             MediaType = g.MediaType,
             FilePath = g.FilePath,
             SortOrder = g.SortOrder
         }).ToList(),
         Documents = x.Asset.TblAssetDocuments.Select(d => new AssetDocumentFormDto
         {
             DocumentId = d.DocumentId,
             DocumentType = d.DocumentType,
             FilePath = d.FilePath
         }).ToList(),
         Attributes = x.Asset.TblAssetDetails.Select(d => new AssetDetailDtoo
         {
             AttributeName = d.AttributeName,
             AttributeValue = d.AttributeValue
         }).ToList()
     })
     .ToListAsync();


            return assets;
        }

        public async Task<List<GetAssetsFormDto>> GetAuctionAllAsync(Expression<Func<TblAsset, bool>> predicate, string languageCode)
        {
            var currentTime = DateTime.Now;

            var language = await _context.TblLanguages
                .Where(l => l.Code == languageCode && l.IsActive)
                .FirstOrDefaultAsync();

            if (language == null)
            {
                throw new Exception("Invalid or inactive language code.");
            }

            int languageId = language.LanguageId;

            var assets = await _context.TblAssets
                .Where(predicate)
                .Where(a => !a.IsDeleted)
                .Include(a => a.Category)
                .Include(a => a.Status)
                .Include(a => a.Vat)
                .Include(a => a.Awarding)
                .Include(a => a.Winner).ThenInclude(w => w.User)
                .Include(a => a.TblAssetGalleries)
                .Include(a => a.TblAssetDocuments)
                .Include(a => a.TblAssetDetails)
                .Include(a => a.TblAuctionAssets).ThenInclude(aa => aa.Auction)
                .Where(a => a.TblAuctionAssets.Any(aa =>
                    !aa.Auction.IsDeleted &&
                    aa.Auction.StartDateTime <= currentTime &&
                    aa.Auction.EndDateTime >= currentTime
                ))
                .GroupJoin(
                    _context.TblAssetTranslations.Where(t => t.LanguageId == languageId),
                    a => a.AssetId,
                    t => t.AssetId,
                    (a, translations) => new { Asset = a, Translation = translations.FirstOrDefault() }
                )
                .Select(x => new GetAssetsFormDto
                {
                    AssetId = x.Asset.AssetId,
                    Title = x.Translation.Title ?? x.Asset.Title,
                    Description = x.Translation.Description ?? x.Asset.Description,
                    SalesNotes = x.Translation.SalesNotes ?? x.Asset.SalesNotes,

                    CategoryId = x.Asset.CategoryId,
                    CategoryName = x.Asset.Category != null ? x.Asset.Category.CategoryName : null,
                    Deposit = x.Asset.Deposit,
                    SellerId = x.Asset.SellerId,
                    Commission = x.Asset.Commission,
                    StartingPrice = x.Asset.StartingPrice,
                    ReserveAmount = x.Asset.ReserveAmount,
                    IncrementalTime = x.Asset.IncrementalTime,
                    MinIncrement = x.Asset.MinIncrement,
                    MakeOffer = x.Asset.MakeOffer,
                    Featured = x.Asset.Featured,
                    AwardingId = x.Asset.AwardingId,
                    AwardingMethod = x.Asset.Awarding != null ? x.Asset.Awarding.AwardingMethod : null,
                    StatusId = x.Asset.StatusId,
                    StatusName = x.Asset.Status != null ? x.Asset.Status.StatusName : null,
                    Vatid = x.Asset.Vatid,
                    VatType = x.Asset.Vat != null ? x.Asset.Vat.Vattype : null,
                    Vatpercent = x.Asset.Vatpercent,
                    CourtCaseNumber = x.Asset.CourtCaseNumber,
                    RegistrationDeadline = x.Asset.RegistrationDeadline,
                    MapLatitude = x.Asset.MapLatitude,
                    MapLongitude = x.Asset.MapLongitude,
                    AdminFees = x.Asset.AdminFees,
                    AuctionFees = x.Asset.AuctionFees,
                    BuyerCommission = x.Asset.BuyerCommission,
                    WinnerId = x.Asset.WinnerId,
                    WinnerName = x.Asset.Winner != null ? x.Asset.Winner.User.Name : null,
                    AwardedPrice = x.Asset.Winner != null ? x.Asset.Winner.AwardedPrice : null,
                    AssetNumber = x.Asset.AssetNumber,
                    CreatedAt = x.Asset.CreatedAt,
                    UpdatedAt = x.Asset.UpdatedAt,
                    IsAvailableForDirectSale = x.Asset.IsAvailableForDirectSale,

                    AuctionId = x.Asset.TblAuctionAssets
                        .Where(aa => !aa.Auction.IsDeleted &&
                                     aa.Auction.StartDateTime <= currentTime &&
                                     aa.Auction.EndDateTime >= currentTime)
                        .Select(aa => (int?)aa.AuctionId)
                        .FirstOrDefault(),

                    Galleries = x.Asset.TblAssetGalleries.Select(g => new AssetGalleryDtos
                    {
                        MediaType = g.MediaType,
                        FilePath = g.FilePath,
                        SortOrder = g.SortOrder
                    }).ToList(),

                    Documents = x.Asset.TblAssetDocuments.Select(d => new AssetDocumentFormDto
                    {
                        DocumentId = d.DocumentId,
                        DocumentType = d.DocumentType,
                        FilePath = d.FilePath
                    }).ToList(),

                    Attributes = x.Asset.TblAssetDetails.Select(d => new AssetDetailDtoo
                    {
                        AttributeName = d.AttributeName,
                        AttributeValue = d.AttributeValue
                    }).ToList()
                })
                .ToListAsync();

            return assets;
        }


        public async Task<TblAsset> AddAsset(TblAsset asset,TblAssetGallery gallery)
        {
            //_context.TblAssets.OrderByDescending(c=>c.AssetId).Select(c => c);

            

            gallery.AssetId = asset.AssetId;
            asset.TblAssetGalleries.Add(gallery);
           _context.TblAssets.Add(asset);
            await _context.SaveChangesAsync();

            return await _context.TblAssets
                .Include(a => a.Category)
                .Include(a => a.Status)
                .Include(a => a.Seller)
                .Include(a => a.Awarding)
                .Include(a => a.Vat)
                .Include(a => a.TblAssetGalleries)
                .Include(a => a.TblAssetDocuments)
                .Include(a => a.TblAssetDetails)
                .FirstOrDefaultAsync(a => a.AssetId == asset.AssetId);  
        }

        public async Task<TblAsset> AddAssetForGallery(TblAsset asset)
        {
            
         
            asset.AssetNumber = await GenerateNextAssetNumberAsync();
            asset.IsDeleted = false;
            _context.TblAssets.Add(asset);
            await _context.SaveChangesAsync();

            return asset;
        }

        public async Task<string> GenerateNextAssetNumberAsync(int startFrom = 1063)
        {
            var maxAssetNumber = await _context.TblAssets
                .Where(a => !a.IsDeleted && a.AssetNumber != null && a.AssetNumber != "")
                .Select(a => (int?)Convert.ToInt32(a.AssetNumber))
                .MaxAsync() ?? (startFrom - 1);

            return (maxAssetNumber + 1).ToString();
        }

        public async Task<bool> HasAnyDirectAndActiveAuctionAsync(int auctionId)
        {
            return await _context.TblAuctions
                .AnyAsync(a =>
                   a.AuctionId == auctionId &&
                   a.Type == "Direct Sale");
        }






        public async Task UpdateAsync(TblAsset asset)
        {
           
            var a = _context.TblAssets.Update(asset);
            Console.WriteLine(a);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TblAsset asset)
        {
            //_context.TblAssets.Remove(asset);
            //    await _context.SaveChangesAsync();

            asset.IsDeleted = true;

            _context.TblAssets.Update(asset);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> AssetsIsExist(int id)
        {
            return await _context.TblAssets.AnyAsync(a => a.AssetId == id);
        }

        public async Task<IEnumerable<TblAsset>> SearchAsset(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Enter product name first");

            }
            var Asset =  await _context.TblAssets
                 .Where(a => a.IsDeleted==false)
                .Include(a => a.Category)
                .Include(a => a.Status)
                .Include(a => a.Seller)
                .Include(a => a.Awarding)
                .Include(a => a.Vat)
                .Include(a => a.TblAssetGalleries)
                .Include(a => a.TblAssetDocuments)
                .Include(a => a.TblAssetDetails)
                .Where(a => a.Title.Contains(name))
                .ToListAsync();

            var terms = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return Asset.Where(
                p => terms.Any(term => p.Title.Contains(term, StringComparison.OrdinalIgnoreCase)
                ||
                p.Description.Contains(term, StringComparison.OrdinalIgnoreCase)));
        }

        public async Task<TblAsset> GetIdDeleteAsync(int id)
            {
            return await _context.TblAssets
                 //.Include(a => a.Category)
                 //.Include(a => a.Status)
                 //.Include(a => a.Seller)
                 //.Include(a => a.Awarding)
                 //.Include(a => a.Vat)
                 //.Include(a => a.TblAssetGalleries)
                 //.Include(a => a.TblAssetDocuments)
                 //.Include(a => a.TblAssetDetails)
                 .FirstOrDefaultAsync(a => a.AssetId == id);
        }

        public Task<TblAsset> AddAsset(TblAsset asset)
        {
            throw new NotImplementedException();
        }

        public async Task DeactivateExpiredAssetsBasedOnDeadlineAsync()
        {
            var now = DateTime.UtcNow.Date;

            
            var assetsToClose = await _context.TblAssets
                .Where(a => a.StatusId != 10
                    && a.CreatedAt != null
                    && a.RegistrationDeadline != null)
                .ToListAsync();

            foreach (var asset in assetsToClose)
            {
               
                var elapsedDays = (now - asset.CreatedAt.Value.Date).Days;

                var remainingDays = asset.RegistrationDeadline.Value - elapsedDays;

                if (remainingDays <= 0)
                {
                   
                    asset.StatusId = 10; 
                }
            }

            await _context.SaveChangesAsync();
        }
        public async Task AddAssetTranslationAsync(tblAssetTranslation translation)
        {
            await _context.TblAssetTranslations.AddAsync(translation);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TblSeller>> getAllSeller()
        {
            return await _context.TblSellers.Include(s=> s.User).ToListAsync();
        }
        public async Task<List<FeaturedAssetDto>> GetFeaturedAssetsAsync()
        {
            var results = new List<FeaturedAssetDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("AuctionM_dbuser.GetFeaturedAssets", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var asset = new FeaturedAssetDto
                            {
                                AssetId = reader.GetInt32(reader.GetOrdinal("AssetId")),
                                Title = reader["Title"] as string,
                                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                CategoryName = reader["CategoryName"] as string,
                                Deposit = Convert.ToDecimal(reader["Deposit"]),
                                SellerId = Convert.ToInt32(reader["SellerId"]),
                                Commission = Convert.ToDecimal(reader["Commission"]),
                                StartingPrice = Convert.ToDecimal(reader["StartingPrice"]),
                                IncrementalTime = Convert.ToInt32(reader["IncrementalTime"]),
                                MinIncrement = Convert.ToDecimal(reader["MinIncrement"]),
                                MakeOffer = Convert.ToBoolean(reader["MakeOffer"]),
                                Featured = Convert.ToBoolean(reader["Featured"]),
                                AwardingId = Convert.ToInt32(reader["AwardingId"]),
                                StatusId = Convert.ToInt32(reader["StatusId"]),
                                StatusName = reader["StatusName"] as string,
                                VATId = Convert.ToInt32(reader["VATId"]),
                                VATPercent = Convert.ToDecimal(reader["VATPercent"]),
                                CourtCaseNumber = reader["CourtCaseNumber"] as string,
                                RegistrationDeadline = reader["RegistrationDeadline"] as DateTime?,
                                Description = reader["Description"] as string,
                                MapLatitude = reader["MapLatitude"] as string,
                                MapLongitude = reader["MapLongitude"] as string,
                                AdminFees = Convert.ToDecimal(reader["AdminFees"]),
                                AuctionFees = Convert.ToDecimal(reader["AuctionFees"]),
                                BuyerCommission = Convert.ToDecimal(reader["BuyerCommission"]),
                                WinnerId = reader["WinnerId"] as int?,
                                AssetNumber = reader["AssetNumber"] as string,
                                RequestForViewing = Convert.ToBoolean(reader["RequestForViewing"]),
                                RequestForInquiry = Convert.ToBoolean(reader["RequestForInquiry"]),
                                GalleryFilePaths = reader["GalleryFilePaths"] as string,
                                DocumentFilePaths = reader["DocumentFilePaths"] as string
                            };
                            results.Add(asset);
                        }
                    }
                }
            }

            return results;
        }


    }
}
