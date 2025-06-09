using System;
using System.Collections.Generic;
using System.Data.Common;
using AuctionManagementSystem.Domain;
using AuctionManagementSystem.Domain.Entities;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Auction;
using AuctionManagementSystem.Domain.Entities.AuditTrail;
using AuctionManagementSystem.Domain.Entities.Bids;
using AuctionManagementSystem.Domain.Entities.Notification;
using AuctionManagementSystem.Domain.Entities.Request;
using AuctionManagementSystem.Domain.Entities.Roles;
using AuctionManagementSystem.Domain.Entities.Settings;
using AuctionManagementSystem.Domain.Entities.Transaction;
using AuctionManagementSystem.Domain.Entities.Translations;
using AuctionManagementSystem.Domain.Entities.User;
using AuctionManagementSystem.Domain.model;
using AuctionManagementSystem.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace AuctionManagementSystem.Persistence.Context;


public partial class AuctionManagementDbContext : DbContext
{
    public AuctionManagementDbContext(DbContextOptions<AuctionManagementDbContext> options)
        : base(options)
    {
    }

    public DbConnection GetDbConnection()
    {
        return Database.GetDbConnection();
    }

    public virtual DbSet<TblAsset> TblAssets { get; set; }

    public virtual DbSet<TblAssetCategory> TblAssetCategories { get; set; }

    public virtual DbSet<TblAssetCategoryStatus> TblAssetCategoryStatuses { get; set; }

    public virtual DbSet<TblAssetDetail> TblAssetDetails { get; set; }

    public virtual DbSet<TblAssetDocument> TblAssetDocuments { get; set; }

    public virtual DbSet<TblAssetGallery> TblAssetGalleries { get; set; }

    public virtual DbSet<TblAssetPaymentMethod> TblAssetPaymentMethods { get; set; }

    public virtual DbSet<TblAssetStatus> TblAssetStatuses { get; set; }

    public virtual DbSet<TblAssetWinner> TblAssetWinners { get; set; }

    public virtual DbSet<TblAuction> TblAuctions { get; set; }

    public virtual DbSet<TblAuctionAsset> TblAuctionAssets { get; set; }

    public virtual DbSet<TblAuctionCategory> TblAuctionCategories { get; set; }

    public virtual DbSet<TblAuctionStatus> TblAuctionStatuses { get; set; }

    public virtual DbSet<TblAuctionView> TblAuctionViews { get; set; }

    public virtual DbSet<TblCardType> TblCardTypes { get; set; }

    public virtual DbSet<TblCountry> TblCountries { get; set; }

    public virtual DbSet<TblDirectSaleSetting> TblDirectSaleSettings { get; set; }

    public virtual DbSet<TblFinanceSetting> TblFinanceSettings { get; set; }

    public virtual DbSet<TblFooterLinksSetting> TblFooterLinksSettings { get; set; }

    public virtual DbSet<TblPaymentMethod> TblPaymentMethods { get; set; }

    public virtual DbSet<TblRequest> TblRequests { get; set; }

    public virtual DbSet<TblRequestStatus> TblRequestStatuses { get; set; }

    public virtual DbSet<TblRequestStatusHistory> TblRequestStatusHistories { get; set; }

    public virtual DbSet<TblRequestType> TblRequestTypes { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblSeller> TblSellers { get; set; }

    public virtual DbSet<TblStaticPagesSettingDto> TblStaticPagesSettings { get; set; }

    public virtual DbSet<TblSystemSetting> TblSystemSettings { get; set; }

    public virtual DbSet<TblTransaction> TblTransactions { get; set; }

    public virtual DbSet<TblTransactionAsset> TblTransactionAssets { get; set; }

    public virtual DbSet<TblAssetCategoryPaymentMethod> TblAssetCategoryPaymentMethods { get; set; }

    public virtual DbSet<TblTransactionDocument> TblTransactionDocuments { get; set; }

    public virtual DbSet<TblTransactionStatus> TblTransactionStatuses { get; set; }

    public virtual DbSet<TblTransactionType> TblTransactionTypes { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblUserRole> TblUserRoles { get; set; }

    public virtual DbSet<TblUserStatus> TblUserStatuses { get; set; }

    public virtual DbSet<TblVatoption> TblVatoptions { get; set; }
    public virtual DbSet<tblAuctionCategoriesTranslations> TblAuctionCategoriesTranslations { get; set; }
   

    public virtual DbSet<TblWinnerAwardingOption> TblWinnerAwardingOptions { get; set; }

    public virtual DbSet<TblWinnerDocument> TblWinnerDocuments { get; set; }

    public virtual DbSet<Faq> Faqs { get; set; }
    public virtual DbSet<tblOTP> tblOTPs { get; set; }
    public virtual DbSet<tblBid> tblBids { get; set; }
    public virtual DbSet<TblAutoBid> TblAutoBids { get; set; }
    public DbSet<TblCartItem> TblCartItems { get; set; }
    public virtual DbSet<TblAuditTrail> TblAuditTrails { get; set; }
    public DbSet<TblWishlistItem> TblWishlistItems { get; set; }
    public virtual DbSet<TblOrder> TblOrders { get; set; }
    public virtual DbSet<TblOrderAsset> TblOrderAssets { get; set; }
    public virtual DbSet<TblRolePermissionsMatrix> TblRolePermissionsMatrices { get; set; }

    public virtual DbSet<TblNotification> TblNotifications { get; set; }
    public virtual DbSet<TblUserDeposit> TblUserDeposits { get; set; }
    public virtual DbSet<tblLanguages> TblLanguages { get; set; }

    public virtual DbSet<TblUserLimitAuditLog> TblUserLimitAuditLogs { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("AuctionM_dbuser");

        modelBuilder.Entity<TblRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__tblRoles__8AFACE1A5E190086");

            entity.ToTable("tblRoles");

            entity.Property(e => e.RoleName)
                .IsRequired()
                .HasMaxLength(50);
        });
        modelBuilder.Entity<tblLanguages>(entity =>
        {
            entity.ToTable("tblLanguages", "AuctionM_dbuser");

            entity.HasKey(e => e.LanguageId)
                  .HasName("PK__Language__3214EC0700FA9F2A");

            entity.HasIndex(e => e.Code)
                  .IsUnique()
                  .HasDatabaseName("UQ__Language__A25C5AA7F398EA3B");

            entity.Property(e => e.LanguageId)
                  .IsRequired();

            entity.Property(e => e.Code)
                  .IsRequired()
                  .HasMaxLength(10);

            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.IsActive)
                  .IsRequired();
        });


        modelBuilder.Entity<TblRolePermissionsMatrix>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__tblRoleP__8AFACE1A931EC603");

            entity.ToTable("tblRolePermissionsMatrix");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.AccessAdminPanel).HasColumnName("Access Admin Panel");
            entity.Property(e => e.ChangeCommission).HasColumnName("Change Commission");
            entity.Property(e => e.ExportReports).HasColumnName("Export Reports");
            entity.Property(e => e.ManageAssets).HasColumnName("Manage Assets");
            entity.Property(e => e.ManageAuctions).HasColumnName("Manage Auctions");
            entity.Property(e => e.ManageCategories).HasColumnName("Manage Categories");
            entity.Property(e => e.ManageRequests).HasColumnName("Manage Requests");
            entity.Property(e => e.ManageRoles).HasColumnName("Manage Roles");
            entity.Property(e => e.ManageTransactions).HasColumnName("Manage Transactions");
            entity.Property(e => e.ManageUsers).HasColumnName("Manage Users");
            entity.Property(e => e.SuperAdmin).HasColumnName("Super Admin");
            entity.Property(e => e.ViewAuditTrail).HasColumnName("View Audit Trail");
            entity.Property(e => e.ViewReports).HasColumnName("View Reports");

            entity.HasOne(d => d.Role).WithOne(p => p.TblRolePermissionsMatrix)
                .HasForeignKey<TblRolePermissionsMatrix>(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblRolePermissionsMatrix_tblRoles");
        });

        modelBuilder.Entity<TblOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__tblOrder__C3905BCF60E8760D");

            entity.ToTable("tblOrders");

            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(100);
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.OrderStatus).HasMaxLength(50);
            entity.Property(e => e.TransactionNumber).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Transaction).WithMany(p => p.TblOrders)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FK_TblOrders_Transactions");

            entity.HasOne(d => d.User).WithMany(p => p.TblOrders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblOrders_Users");
        });

        modelBuilder.Entity<TblOrderAsset>(entity =>
        {
            entity.HasKey(e => e.OrderAssetId).HasName("PK__tblOrder__4D4B2C4696255B49");

            entity.ToTable("tblOrderAssets");

            entity.HasOne(d => d.Asset).WithMany(p => p.TblOrderAssets)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblOrderAssets_Assets");

            entity.HasOne(d => d.Order).WithMany(p => p.TblOrderAssets)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblOrderAssets_Orders");
        });

        // For CartItem mapping
        modelBuilder.Entity<TblCartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemId).HasName("PK__tblCartI__488B0B0A0437EB75");

            entity.ToTable("tblCartItems");

            entity.Property(e => e.AddedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Quantity).HasDefaultValue(1);

            entity.HasOne(d => d.Asset).WithMany(p => p.TblCartItems)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblCartIt__Asset__2E3BD7D3");

            entity.HasOne(d => d.User).WithMany(p => p.TblCartItems)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblCartIt__UserI__2D47B39A");
        });
        modelBuilder.Entity<TblNotification>()
            .HasOne(n => n.User)  
            .WithMany(u => u.TblNotifications)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        // For WishlistItem mapping
        modelBuilder.Entity<TblWishlistItem>(entity =>
        {
            entity.HasKey(e => e.WishlistItemId).HasName("PK__tblWishl__171E21A16D425329");

            entity.ToTable("tblWishlistItems");

            entity.Property(e => e.AddedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Asset).WithMany(p => p.TblWishlistItems)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblWishli__Asset__2882FE7D");

            entity.HasOne(d => d.User).WithMany(p => p.TblWishlistItems)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblWishli__UserI__278EDA44");
        });

        

        modelBuilder.Entity<tblOTP>(entity =>
        {
            entity.ToTable("tblOTPs");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Code)
                  .IsRequired()
                  .HasMaxLength(6);

            entity.Property(e => e.Expiration)
                  .IsRequired();

            entity.Property(e => e.IsUsed)
                  .HasDefaultValue(false);

            entity.HasOne(e => e.User)
                  .WithMany(u => u.OTPs)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });


        modelBuilder.Entity<TblAsset>(entity =>
        {
            entity.HasKey(e => e.AssetId).HasName("PK__tblAsset__43492352851DA4A7");

            entity.ToTable("tblAssets");

            entity.Property(e => e.AdminFees).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AssetNumber).HasMaxLength(100);
            entity.Property(e => e.AuctionFees).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BuyerCommission).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Commission).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.CourtCaseNumber).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deposit).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Featured).HasDefaultValue(false);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.MakeOffer).HasDefaultValue(false);
            entity.Property(e => e.MapLatitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.MapLongitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.MinIncrement).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RequestForInquiry).HasDefaultValue(true);
            entity.Property(e => e.RequestForViewing).HasDefaultValue(true);
            entity.Property(e => e.ReserveAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StartingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Vatid).HasColumnName("VATId");
            entity.Property(e => e.Vatpercent)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("VATPercent");

            entity.HasOne(d => d.Awarding).WithMany(p => p.TblAssets)
                .HasForeignKey(d => d.AwardingId)
                .HasConstraintName("FK__tblAssets__Award__17036CC0");

            entity.HasOne(d => d.Category).WithMany(p => p.TblAssets)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__tblAssets__Categ__114A936A");

            entity.HasOne(d => d.Seller).WithMany(p => p.TblAssets)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblAssets__Selle__1332DBDC");

            entity.HasOne(d => d.Status).WithMany(p => p.TblAssets)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__tblAssets__Statu__17F790F9");

            entity.HasOne(d => d.Vat).WithMany(p => p.TblAssets)
                .HasForeignKey(d => d.Vatid)
                .HasConstraintName("FK__tblAssets__VATId__18EBB532");
            entity.Property(a => a.IsAvailableForDirectSale)
                .HasColumnName("IsAvailableForDirectSale")
                .HasDefaultValue(false);
            entity.HasOne(a => a.Winner)
                .WithOne(w => w.Asset)
                .HasForeignKey<TblAsset>(a => a.WinnerId)
                .HasConstraintName("FK_tblAssets_WinnerId");
        });

        modelBuilder.Entity<TblAssetCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__tblAsset__19093A0B90992811");

            entity.ToTable("tblAssetCategories");

            entity.HasIndex(e => e.CategoryName, "UQ__tblAsset__8517B2E06B149E78").IsUnique();

            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Subcategory)
                .HasMaxLength(255);

            entity.Property(e => e.DepositPercentage)
                .HasColumnType("decimal(5, 2)");

            entity.Property(e => e.Details);

            entity.Property(e => e.AdminFees)
                .HasColumnType("decimal(18, 2)");

            entity.Property(e => e.AuctionFees)
                .HasColumnType("decimal(18, 2)");

            entity.Property(e => e.BuyerCommission)
                .HasColumnType("decimal(18, 2)");

            entity.Property(e => e.RegistrationDeadline)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.Icon)
                .IsRequired()
                .HasMaxLength(255)
                .HasDefaultValue("default.png");

            entity.Property(e => e.DocumentPath)
                .HasMaxLength(255)
                .HasDefaultValue(null);

            entity.Property(e => e.Vatpercentage)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("VATPercentage");

            entity.Property(e => e.Vatid)
                .HasColumnName("VATId");

            entity.Property(e => e.StatusId)
                .HasDefaultValue(1);

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100);

            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100);

            entity.Property(e => e.DeletedBy)
                .HasMaxLength(100);

            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime");

            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false);

            entity.HasOne(d => d.Status)
                .WithMany(p => p.TblAssetCategories)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblAssetCategories_tblStatus");

            entity.HasOne(d => d.Vat)
                .WithMany(p => p.TblAssetCategories)
                .HasForeignKey(d => d.Vatid)
                .HasConstraintName("FK_tblAssetCategories_VATId");

            // ✅ Removed: HasMany-WithMany using Dictionary

            // ✅ Add navigation for explicit join entity configuration
            entity.HasMany(d => d.TblAssetCategoryPaymentMethods)
                .WithOne(pm => pm.Category)
                .HasForeignKey(pm => pm.CategoryId)
                .HasConstraintName("FK_TblAssetCategoryPaymentMethod_Category");
        });

        modelBuilder.Entity<TblAssetCategoryPaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentOptionId);

            entity.ToTable("TblAssetCategoryPaymentMethod");

            entity.HasOne(d => d.Category).WithMany(p => p.TblAssetCategoryPaymentMethods)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__TblAssetC__Categ__3DE82FB7");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.TblAssetCategoryPaymentMethods)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK__TblAssetC__Payme__3EDC53F0");
        });


        modelBuilder.Entity<TblAssetCategoryStatus>(entity =>
        {
            entity.HasKey(e => e.AssetCategoryStatusId).HasName("PK__TblAsset__5DF10922B7703246");

            entity.ToTable("TblAssetCategoryStatus");

            entity.Property(e => e.AssetCategoryStatusId).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<TblAssetDetail>(entity =>
        {
            entity.HasKey(e => e.DetailId).HasName("PK__tblAsset__135C316D8D6089A4");

            entity.ToTable("tblAssetDetails");

            entity.Property(e => e.AttributeName).HasMaxLength(100);
            entity.Property(e => e.AttributeValue).HasMaxLength(255);

            entity.HasOne(d => d.Asset).WithMany(p => p.TblAssetDetails)
                .HasForeignKey(d => d.AssetId)
                .HasConstraintName("FK__tblAssetD__Asset__339FAB6E");
        });

        modelBuilder.Entity<TblAssetDocument>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__tblAsset__1ABEEF0FA6518C70");

            entity.ToTable("tblAssetDocuments");

            entity.Property(e => e.DocumentType).HasMaxLength(100);

            entity.HasOne(d => d.Asset).WithMany(p => p.TblAssetDocuments)
                .HasForeignKey(d => d.AssetId)
                .HasConstraintName("FK__tblAssetD__Asset__30C33EC3");
        });

        modelBuilder.Entity<TblAssetGallery>(entity =>
        {
            entity.HasKey(e => e.GalleryId).HasName("PK__tblAsset__CF4F7BB511FCD16B");

            entity.ToTable("tblAssetGallery");

            entity.Property(e => e.MediaType).HasMaxLength(10);

            entity.HasOne(d => d.Asset).WithMany(p => p.TblAssetGalleries)
                .HasForeignKey(d => d.AssetId)
                .HasConstraintName("FK__tblAssetG__Asset__1EA48E88");
        });

        modelBuilder.Entity<TblAssetPaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TblAsset__3214EC072B4018C6");

            entity.ToTable("TblAssetPaymentMethod");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<TblAssetStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__tblAsset__C8EE2063D0439541");

            entity.ToTable("tblAssetStatus");

            entity.HasIndex(e => e.StatusName, "UQ__tblAsset__05E7698A4E67696A").IsUnique();

            entity.Property(e => e.StatusName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<TblAssetWinner>(entity =>
        {
            entity.HasKey(e => e.WinnerId).HasName("PK__tblAsset__8A3D1DA877CE62B9");

            entity.ToTable("tblAssetWinners");

            entity.Property(e => e.Approved).HasDefaultValue(false);
            entity.Property(e => e.AwardedPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(255);

            entity.HasOne(d => d.Asset)
                 .WithOne(p => p.Winner)
                 .HasForeignKey<TblAsset>(a => a.WinnerId)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("FK_tblAssetWinners_AssetId");

            entity.HasOne(d => d.User).WithMany(p => p.TblAssetWinners)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__tblAssetW__UserI__282DF8C2");
            entity.Property(e => e.IsSeen)
                .HasDefaultValue(false)
                .IsRequired();
        });
        modelBuilder.Entity<TblAuditTrail>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("PK__tblAudit__A17F239830D9A9C0");

            entity.ToTable("tblAuditTrail");

            entity.Property(e => e.ActivityPerformedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ChangeType).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(100);
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.ModelName).HasMaxLength(100);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Role).WithMany(p => p.TblAuditTrails)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditTrail_RoleId");

            entity.HasOne(d => d.User).WithMany(p => p.TblAuditTrails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditTrail_UserId");
        });


        modelBuilder.Entity<TblAuction>(entity =>
        {
            entity.HasKey(e => e.AuctionId).HasName("PK__tblAucti__51004A4C8D65AE77");

            entity.ToTable("tblAuctions");

            entity.HasIndex(e => e.AuctionNumber, "UQ_TblAuctions_AuctionNumber").IsUnique();

            entity.Property(e => e.AuctionNumber)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(100);
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.EndDateTime).HasColumnType("datetime");
            entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasDefaultValue("Auction");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.TblAuctions)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_tblAssetCategories_CategoryId");
        });

        modelBuilder.Entity<TblAuctionAsset>(entity =>
        {
            entity.HasKey(e => e.AuctionAssetId).HasName("PK__tblAucti__3E3E62EC12EA2666");

            entity.ToTable("tblAuctionAssets");

            entity.HasOne(d => d.Asset).WithMany(p => p.TblAuctionAssets)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblAuctionAssets_AssetId");

            entity.HasOne(d => d.Auction).WithMany(p => p.TblAuctionAssets)
                .HasForeignKey(d => d.AuctionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblAuctio__Aucti__66603565");
        });

        modelBuilder.Entity<TblAuctionCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__tblAucti__19093A0B5FA7FC15");

            entity.ToTable("tblAuctionCategories");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<TblAuctionStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__tblAucti__C8EE20639F7FF226");

            entity.ToTable("tblAuctionStatuses");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<TblAuctionView>(entity =>
        {
            entity.HasKey(e => e.AuctionViewId).HasName("PK__tblAucti__B54FF0B6D3D5306E");

            entity.ToTable("tblAuctionViews");

            entity.Property(e => e.ViewedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Auction).WithMany(p => p.TblAuctionViews)
                .HasForeignKey(d => d.AuctionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblAuctio__Aucti__6B24EA82");
        });


        modelBuilder.Entity<TblAutoBid>(entity =>
        {
            entity.HasKey(e => e.AutoBidId).HasName("PK__tblAutoB__C5E25909977EF2B6");

            entity.ToTable("tblAutoBids");

            entity.HasIndex(e => new { e.UserId, e.AuctionId, e.AssetId }, "UQ_tblAutoBid_User_Auction_Asset").IsUnique();

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MaxBidAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Asset).WithMany(p => p.TblAutoBids)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblAutoBid_Asset");

            entity.HasOne(d => d.Auction).WithMany(p => p.TblAutoBids)
                .HasForeignKey(d => d.AuctionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblAutoBid_Auction");

            entity.HasOne(d => d.User).WithMany(p => p.TblAutoBids)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblAutoBid_User");
        });


        //modelBuilder.Entity<tblBid>(entity =>
        //{
        //    entity.HasKey(e => e.BidId).HasName("PK__tblBids__4A733D920BE15F30");

        //    entity.ToTable("tblBids");

        //    entity.Property(e => e.BidAmount).HasColumnType("decimal(10, 2)");
        //    entity.Property(e => e.BidTime)
        //        .HasDefaultValueSql("(getdate())")
        //        .HasColumnType("datetime");
        //    entity.Property(e => e.CreatedDate)
        //        .HasDefaultValueSql("(getdate())")
        //        .HasColumnType("datetime");
        //    entity.Property(e => e.IsWinningBid).HasDefaultValue(false);

        //    entity.HasOne(d => d.Asset).WithMany(p => p.TblBids)
        //        .HasForeignKey(d => d.AssetId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_Bids_Assets");

        //    entity.HasOne(d => d.Auction).WithMany(p => p.TblBids)
        //        .HasForeignKey(d => d.AuctionId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_Bids_Auctions");

        //    entity.HasOne(d => d.User).WithMany(p => p.TblBids)
        //        .HasForeignKey(d => d.UserId)

        modelBuilder.Entity<tblBid>(entity =>
        {
            entity.ToTable("tblBids");

            entity.HasKey(e => e.BidId);

            entity.Property(e => e.BidId)
                  .ValueGeneratedOnAdd();

            entity.Property(e => e.BidAmount)
                  .HasColumnType("decimal(10,2)")
                  .IsRequired();

            entity.Property(e => e.BidTime)
                  .IsRequired()
                  .HasDefaultValueSql("GETDATE()");

            entity.Property(e => e.IsWinningBid)
                  .IsRequired()
                  .HasDefaultValue(false);

            entity.Property(e => e.CreatedDate)
                  .IsRequired()
                  .HasDefaultValueSql("GETDATE()");

            // Fix the relationship configuration by explicitly specifying foreign key properties
            entity.HasOne(d => d.Auction)
                  .WithMany(p => p.TblBids)  // Assuming TblAuction has a TblBids collection
                  .HasForeignKey(d => d.AuctionId)
                  .HasConstraintName("FK_tblBids_tblAuction")
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Asset)
                  .WithMany(p => p.TblBids)  // Assuming TblAsset has a TblBids collection
                  .HasForeignKey(d => d.AssetId)
                  .HasConstraintName("FK_tblBids_tblAsset")
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.User)
                  .WithMany(p => p.TblBids)  // This matches what we see in your TblUser class
                  .HasForeignKey(d => d.UserId)
                  .HasConstraintName("FK_tblBids_tblUser")
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<TblCardType>(entity =>
        {
            entity.HasKey(e => e.CardTypeId).HasName("PK__tblCardT__AB0A3D117F774D59");

            entity.ToTable("tblCardTypes");

            entity.HasIndex(e => e.CardTypeName, "UQ__tblCardT__7F70364E6610CB47").IsUnique();

            entity.Property(e => e.CardTypeName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblCountry>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__tblCount__10D1609F81F3F3B3");

            entity.ToTable("tblCountries");

            entity.Property(e => e.CountryName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.MaxLength).HasDefaultValue(10);
            entity.Property(e => e.MinLength).HasDefaultValue(10);
            entity.Property(e => e.PhoneCode)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.SeriesStart)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblDirectSaleSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblDirec__3214EC075F4D20A8");

            entity.ToTable("tblDirectSaleSettings");
        });

        modelBuilder.Entity<TblFinanceSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblFinan__3214EC07F26E9E1D");

            entity.ToTable("tblFinanceSettings");

            entity.Property(e => e.AdminFees).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.AuctionFees).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BuyerCommissionPercent).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.CreditCardFee).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.DebitCardFee).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Vatpercent)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("VATPercent");
        });

        modelBuilder.Entity<TblFooterLinksSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblFoote__3214EC07EA5009B3");

            entity.ToTable("tblFooterLinksSettings");

            entity.Property(e => e.AppStoreLink).HasMaxLength(255);
            entity.Property(e => e.Blog).HasMaxLength(255);
            entity.Property(e => e.FacebookLink).HasMaxLength(255);
            entity.Property(e => e.Faq)
                .HasMaxLength(255)
                .HasColumnName("FAQ");
            entity.Property(e => e.GooglePlayLink).HasMaxLength(255);
            entity.Property(e => e.InstagramLink).HasMaxLength(255);
            entity.Property(e => e.LinkedInLink).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.TwitterLink).HasMaxLength(255);
            entity.Property(e => e.YouTubeLink).HasMaxLength(255);
        });

        modelBuilder.Entity<TblPaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__tblPayme__DC31C1D36E6B9DFB");

            entity.ToTable("tblPaymentMethods");

            entity.HasIndex(e => e.PaymentMethodName, "UQ__tblPayme__612080EDB9885BC1").IsUnique();

            entity.Property(e => e.PaymentMethodName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });
        modelBuilder.Entity<TblRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__tblReque__33A8517AFFB28830");

            entity.ToTable("tblRequests");

            entity.HasIndex(e => e.RequestNumber, "UQ__tblReque__9ADA6BE06F914861").IsUnique();

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(100);
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RequestDateTime).HasColumnType("datetime");
            entity.Property(e => e.RequestNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Asset).WithMany(p => p.TblRequests)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblReques__Asset__7FEAFD3E");

            entity.HasOne(d => d.RequestStatus).WithMany(p => p.TblRequests)
                .HasForeignKey(d => d.RequestStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblReques__Reque__02C769E9");

            entity.HasOne(d => d.RequestType).WithMany(p => p.TblRequests)
                .HasForeignKey(d => d.RequestTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblReques__Reque__01D345B0");

            entity.HasOne(d => d.Transaction).WithMany(p => p.TblRequests)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FK__tblReques__Trans__00DF2177");

            entity.HasOne(d => d.User).WithMany(p => p.TblRequests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblReques__UserI__7EF6D905");
        });

        modelBuilder.Entity<TblRequestStatus>(entity =>
        {
            entity.HasKey(e => e.RequestStatusId).HasName("PK__tblReque__7094B79B7A164D64");

            entity.ToTable("tblRequestStatuses");

            entity.Property(e => e.StatusName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.RequestType).WithMany(p => p.TblRequestStatuses)
                .HasForeignKey(d => d.RequestTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblReques__Reque__7849DB76");
        });

        modelBuilder.Entity<TblRequestStatusHistory>(entity =>
        {
            entity.HasKey(e => e.StatusHistoryId).HasName("PK__tblReque__DB973491C09D1BC3");

            entity.ToTable("tblRequestStatusHistory");

            entity.Property(e => e.ChangeDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.NewRequestStatus).WithMany(p => p.TblRequestStatusHistoryNewRequestStatuses)
                .HasForeignKey(d => d.NewRequestStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblReques__NewRe__0880433F");

            entity.HasOne(d => d.PreviousRequestStatus).WithMany(p => p.TblRequestStatusHistoryPreviousRequestStatuses)
                .HasForeignKey(d => d.PreviousRequestStatusId)
                .HasConstraintName("FK__tblReques__Previ__078C1F06");

            entity.HasOne(d => d.Request).WithMany(p => p.TblRequestStatusHistories)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblReques__Reque__0697FACD");
        });

        modelBuilder.Entity<TblRequestType>(entity =>
        {
            entity.HasKey(e => e.RequestTypeId).HasName("PK__tblReque__4D328B836DD3F310");

            entity.ToTable("tblRequestTypes");

            entity.HasIndex(e => e.TypeName, "UQ__tblReque__D4E7DFA8911F864D").IsUnique();

            entity.Property(e => e.TypeName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__tblRoles__8AFACE1A5E190086");

            entity.ToTable("tblRoles");

            entity.Property(e => e.RoleName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<TblSeller>(entity =>
        {
            entity.HasKey(e => e.SellerId).HasName("PK__tblSelle__7FE3DB81818038FE");

            entity.ToTable("tblSellers");

            entity.HasOne(d => d.User).WithMany(p => p.TblSellers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__tblSeller__UserI__02FC7413");
        });

        modelBuilder.Entity<TblStaticPagesSettingDto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblStati__3214EC07214212C0");

            entity.ToTable("tblStaticPagesSettings");

            entity.Property(e => e.CookiesPolicy).HasColumnType("text");
            entity.Property(e => e.PrivacyPolicy).HasColumnType("text");
            entity.Property(e => e.TermsAndConditions).HasColumnType("text");
        });

        modelBuilder.Entity<TblSystemSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblSyste__3214EC07A3300712");

            entity.ToTable("tblSystemSettings");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });


        modelBuilder.Entity<TblTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__tblTrans__55433A6B2E2D976E");

            entity.ToTable("tblTransactions");

            entity.HasIndex(e => e.TransactionNumber, "UQ__tblTrans__E733A2BFC8CBFDE8").IsUnique();

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

            // Old audit columns (still mapped in DB, optional to keep)
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.CreatedByAdminId).HasColumnName("CreatedByAdminID");

            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            // New audit fields
            entity.Property(e => e.CreatedBy).HasColumnType("int");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("(getdate())");

            entity.Property(e => e.UpdatedBy).HasColumnType("int");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime").HasDefaultValueSql("(getdate())");

            entity.Property(e => e.DeletedBy).HasColumnType("int");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");

            entity.Property(e => e.IsDeleted).HasColumnType("bit").HasDefaultValue(false);

            entity.Property(e => e.DocumentPath)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.MerchantTransactionId)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Notes).HasColumnType("text");

            entity.Property(e => e.TransactionDateTime).HasColumnType("datetime");

            entity.Property(e => e.TransactionNumber)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            // Relationships
            entity.HasOne(d => d.CardType).WithMany(p => p.TblTransactions)
                .HasForeignKey(d => d.CardTypeId)
                .HasConstraintName("FK__tblTransa__CardT__56E8E7AB");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.TblTransactions)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblTransa__Payme__55F4C372");

            entity.HasOne(d => d.Status).WithMany(p => p.TblTransactions)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblTransa__Statu__57DD0BE4");

            entity.HasOne(d => d.TransactionType).WithMany(p => p.TblTransactions)
                .HasForeignKey(d => d.TransactionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblTransa__Trans__55009F39");

            entity.HasOne(d => d.User).WithMany(p => p.TblTransactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblTransa__UserI__540C7B00");

            entity.HasOne(t => t.UpdatedByUser)
      .WithMany()
      .HasForeignKey(t => t.UpdatedBy)
      .OnDelete(DeleteBehavior.Restrict);

        });

        modelBuilder.Entity<TblTransactionAsset>(entity =>
        {
            entity.HasKey(e => e.TransactionAssetsId).HasName("PK__tblTrans__835ED2B593C79289");

            entity.ToTable("tblTransactionAssets");

            entity.Property(e => e.BidPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Asset).WithMany(p => p.TblTransactionAssets)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblTransa__Asset__607251E5");

            entity.HasOne(d => d.Transaction).WithMany(p => p.TblTransactionAssets)
                .HasForeignKey(d => d.TransactionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblTransa__Trans__5F7E2DAC");
        });

        modelBuilder.Entity<TblTransactionDocument>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__tblTrans__1ABEEF0F9B5905EF");

            entity.ToTable("tblTransactionDocuments");

            entity.Property(e => e.DocumentType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FilePath)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Transaction).WithMany(p => p.TblTransactionDocuments)
                .HasForeignKey(d => d.TransactionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblTransa__Trans__5BAD9CC8");
        });

        modelBuilder.Entity<TblTransactionStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__tblTrans__C8EE206351457432");

            entity.ToTable("tblTransactionStatuses");

            entity.HasIndex(e => e.StatusName, "UQ__tblTrans__05E7698A4561ED20").IsUnique();

            entity.Property(e => e.StatusName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblTransactionType>(entity =>
        {
            entity.HasKey(e => e.TransactionTypeId).HasName("PK__tblTrans__20266D0B25AD456E");

            entity.ToTable("tblTransactionTypes");

            entity.HasIndex(e => e.TransactionTypeName, "UQ__tblTrans__2BE3AC2432109A6B").IsUnique();

            entity.Property(e => e.TransactionTypeName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblUserDeposit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TblUserD__3214EC07D2270BC5");

            entity.ToTable("TblUserDeposit");

            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(100);
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.DepositAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ModifiedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });


        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tblUsers__1788CC4CA0D5030C");

            entity.ToTable("tblUsers");

            entity.HasIndex(e => e.Uid, "UQ__tblUsers__C5B19603F27D43C6").IsUnique();

            entity.Property(e => e.AvailableLimit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChatEnabled).HasDefaultValue(true);
            entity.Property(e => e.CompanyName).HasMaxLength(255);
            entity.Property(e => e.CompanyNumber).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(100);
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Deposit)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.LastOnline).HasColumnType("datetime");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.PersonalIdExpiryDate).HasColumnType("date");
            entity.Property(e => e.PersonalIdImage).HasMaxLength(255);
            entity.Property(e => e.PersonalIdNumber)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ProfileImage).HasMaxLength(255);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TotalLimit)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });


        modelBuilder.Entity<TblUserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__tblUserR__3D978A3508215A05");

            entity.ToTable("tblUserRoles");

            entity.Property(e => e.AssignedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Role).WithMany(p => p.TblUserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblUserRo__RoleI__38996AB5");

            entity.HasOne(d => d.User).WithMany(p => p.TblUserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblUserRo__UserI__37A5467C");
        });

        modelBuilder.Entity<TblUserStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__tblUserS__C8EE20631CC56B9E");

            entity.ToTable("tblUserStatus");

            entity.Property(e => e.StatusName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<TblVatoption>(entity =>
        {
            entity.HasKey(e => e.Vatid).HasName("PK__tblVATOp__4A96282EABE16FDE");

            entity.ToTable("tblVATOptions");

            entity.HasIndex(e => e.Vattype, "UQ__tblVATOp__68B32E85E8A08FFD").IsUnique();

            entity.Property(e => e.Vatid).HasColumnName("VATId");
            entity.Property(e => e.Vattype)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("VATType");
        });

        modelBuilder.Entity<TblWinnerAwardingOption>(entity =>
        {
            entity.HasKey(e => e.AwardingId).HasName("PK__tblWinne__F43393FB4EEF2A31");

            entity.ToTable("tblWinnerAwardingOptions");

            entity.HasIndex(e => e.AwardingMethod, "UQ__tblWinne__5B5DB46B5BED39DB").IsUnique();

            entity.Property(e => e.AwardingMethod)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<TblWinnerDocument>(entity =>
        {
            entity.HasKey(e => e.WinnerDocumentId).HasName("PK__tblWinne__E2A7DE31A1585503");

            entity.ToTable("tblWinnerDocuments");

            entity.Property(e => e.DocumentType).HasMaxLength(100);
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Winner).WithMany(p => p.TblWinnerDocuments)
                .HasForeignKey(d => d.WinnerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_tblWinnerDocuments_WinnerId");
        });

        modelBuilder.Entity<Faq>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Question).IsRequired();
            entity.Property(e => e.Answer).IsRequired();
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.Tags).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TblUserLimitAuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditLogId).HasName("PK__tblUserL__EB5F6CBDDF0E18DD");

            entity.ToTable("tblUserLimitAuditLog");

            entity.Property(e => e.ActionType)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.ChangedBy).HasMaxLength(100);
            entity.Property(e => e.ChangedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NewAvailableLimit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NewDeposit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NewTotalLimit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.OldAvailableLimit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OldDeposit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OldTotalLimit).HasColumnType("decimal(18, 2)");
        });

        //modelBuilder.Entity<Tbltempdatum>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("PK__tbltempd__3213E83F07257A51");

        //    entity.ToTable("tbltempdata");

        //    entity.Property(e => e.Id).HasColumnName("id");
        //    entity.Property(e => e.Name)
        //        .HasMaxLength(1)
        //        .HasColumnName("name");
        //});

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}