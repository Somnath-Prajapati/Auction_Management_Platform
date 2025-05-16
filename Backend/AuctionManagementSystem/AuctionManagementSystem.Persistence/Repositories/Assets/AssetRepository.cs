using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.Assets
{
    public class AssetRepository : IAssetsRepository
    {
        private readonly AuctionManagementDbContext _context;

        public AssetRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<TblAsset>> GetAllAsync()
        //{
        //    return await _context.TblAssets
        //        .Include(a => a.Category)
        //        .Include(a => a.Status)
        //        .Include(a => a.Seller)
        //        .Include(a => a.Awarding)
        //        .Include(a => a.Vat)
        //        .Include(a => a.TblAssetGalleries)
        //        .Include(a => a.TblAssetDocuments)
        //        .Include(a => a.TblAssetDetails)
        //        .ToListAsync();
        //}


        public async Task<IEnumerable<GetAssetsFormDto>> GetAllAsync()
        {
                var assets = await _context.TblAssets
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

        public async Task<List<GetAssetsFormDto>> GetDirectAllAsync(Expression<Func<TblAsset, bool>> predicate)
        {
            var assets = await _context.TblAssets
                .Where(predicate)
                .Where(a => a.IsDeleted == false)
                .Include(a => a.Category)
                .Include(a => a.Status)
                .Include(a => a.Vat)
                .Include(a => a.Awarding)
                .Include(a => a.Winner)
                .Include(a => a.TblAssetGalleries)
                .Include(a => a.TblAssetDocuments)
                .Include(a => a.TblAssetDetails)

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
                    AuctionStatusId = a.TblAuctionAssets.Select(aa => aa.Auction.StatusId).FirstOrDefault(),

                    IsAvailableForDirectSale = a.IsAvailableForDirectSale,
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

        public async Task<List<GetAssetsFormDto>> GetAuctionAllAsync(Expression<Func<TblAsset, bool>> predicate)
        {
            var currentTime = DateTime.Now;

            var assets = await _context.TblAssets
                .Where(predicate)
                .Where(a => !a.IsDeleted)
                .Include(a => a.Category)
                .Include(a => a.Status)
                .Include(a => a.Vat)
                .Include(a => a.Awarding)
                .Include(a => a.Winner)
                .Include(a => a.TblAssetGalleries)
                .Include(a => a.TblAssetDocuments)
                .Include(a => a.TblAssetDetails)
                .Include(a => a.TblAuctionAssets)
                    .ThenInclude(aa => aa.Auction)
                // Filter assets that belong to a valid auction
                .Where(a => a.TblAuctionAssets.Any(aa =>
                    !aa.Auction.IsDeleted &&
                    aa.Auction.StartDateTime <= currentTime &&
                    aa.Auction.EndDateTime >= currentTime
                ))
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
                    WinnerName = a.Winner != null ? a.Winner.User.Name : null,
                    AwardedPrice = a.Winner != null ? a.Winner.AwardedPrice : null,
                    SalesNotes = a.SalesNotes,
                    AssetNumber = a.AssetNumber,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt,
                    IsAvailableForDirectSale = a.IsAvailableForDirectSale,
                    AuctionId = a.TblAuctionAssets
                                .Where(aa =>
                                    !aa.Auction.IsDeleted &&
                                    aa.Auction.StartDateTime <= currentTime &&
                                    aa.Auction.EndDateTime >= currentTime)
                                .Select(aa => (int?)aa.AuctionId)
                                .FirstOrDefault(),
                    Galleries = a.TblAssetGalleries.Select(g => new AssetGalleryDtos
                    {
                        MediaType = g.MediaType,
                        FilePath = g.FilePath,
                        SortOrder = g.SortOrder
                    }).ToList(),
                    Documents = a.TblAssetDocuments.Select(d => new AssetDocumentFormDto
                    {
                        DocumentId = d.DocumentId,
                        DocumentType = d.DocumentType,
                        FilePath = d.FilePath
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



        public async Task<int> AddAssetForGallery(TblAsset asset)
        {
            asset.IsDeleted = false;
            _context.TblAssets.Add(asset);
            await _context.SaveChangesAsync();

            return asset.AssetId;
        }
        public async Task UpdateAsync(TblAsset asset)
        {
            asset.UpdatedAt = DateTime.UtcNow;
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
    }
}
