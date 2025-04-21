using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.TempEntities;

public partial class AuctionManagementDbContext : DbContext
{
    public AuctionManagementDbContext()
    {
    }

    public AuctionManagementDbContext(DbContextOptions<AuctionManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAsset> TblAssets { get; set; }

    public virtual DbSet<TblAssetCategory> TblAssetCategories { get; set; }

    public virtual DbSet<TblAssetDetail> TblAssetDetails { get; set; }

    public virtual DbSet<TblAssetDocument> TblAssetDocuments { get; set; }

    public virtual DbSet<TblAssetGallery> TblAssetGalleries { get; set; }

    public virtual DbSet<TblAssetStatus> TblAssetStatuses { get; set; }

    public virtual DbSet<TblAssetWinner> TblAssetWinners { get; set; }

    public virtual DbSet<TblAuction> TblAuctions { get; set; }

    public virtual DbSet<TblAuctionAsset> TblAuctionAssets { get; set; }

    public virtual DbSet<TblAuctionCategory> TblAuctionCategories { get; set; }

    public virtual DbSet<TblAuctionStatus> TblAuctionStatuses { get; set; }

    public virtual DbSet<TblAuctionView> TblAuctionViews { get; set; }

    public virtual DbSet<TblCountry> TblCountries { get; set; }

    public virtual DbSet<TblDirectSaleSetting> TblDirectSaleSettings { get; set; }

    public virtual DbSet<TblFinanceSetting> TblFinanceSettings { get; set; }

    public virtual DbSet<TblFooterLinksSetting> TblFooterLinksSettings { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblSeller> TblSellers { get; set; }

    public virtual DbSet<TblStaticPagesSetting> TblStaticPagesSettings { get; set; }

    public virtual DbSet<TblSystemSetting> TblSystemSettings { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblUserRole> TblUserRoles { get; set; }

    public virtual DbSet<TblUserStatus> TblUserStatuses { get; set; }

    public virtual DbSet<TblVatoption> TblVatoptions { get; set; }

    public virtual DbSet<TblWinnerAwardingOption> TblWinnerAwardingOptions { get; set; }

    public virtual DbSet<TblWinnerDocument> TblWinnerDocuments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=180.149.240.247;Database=AuctionManagementDB;User Id=AuctionM_dbuser;Password=AuctionManagementDB@2025;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("AuctionM_dbuser");

        modelBuilder.Entity<TblAsset>(entity =>
        {
            entity.HasKey(e => e.AssetId).HasName("PK__tblAsset__43492352851DA4A7");

            entity.ToTable("tblAssets");

            entity.Property(e => e.AdminFees).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AuctionFees).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BuyerCommission).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Commission).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.CourtCaseNumber).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deposit).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Featured).HasDefaultValue(false);
            entity.Property(e => e.MakeOffer).HasDefaultValue(false);
            entity.Property(e => e.MapLatitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.MapLongitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.MinIncrement).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ReserveAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StartingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Title).HasMaxLength(255);
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
        });

        modelBuilder.Entity<TblAssetCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__tblAsset__19093A0B90992811");

            entity.ToTable("tblAssetCategories");

            entity.HasIndex(e => e.CategoryName, "UQ__tblAsset__8517B2E06B149E78").IsUnique();

            entity.Property(e => e.CategoryName).HasMaxLength(100);
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

        modelBuilder.Entity<TblAssetStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__tblAsset__C8EE2063D0439541");

            entity.ToTable("tblAssetStatus");

            entity.HasIndex(e => e.StatusName, "UQ__tblAsset__05E7698A4E67696A").IsUnique();

            entity.Property(e => e.StatusName).HasMaxLength(50);
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

            entity.HasOne(d => d.Asset).WithMany(p => p.TblAssetWinners)
                .HasForeignKey(d => d.AssetId)
                .HasConstraintName("FK__tblAssetW__Asset__2739D489");

            entity.HasOne(d => d.User).WithMany(p => p.TblAssetWinners)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__tblAssetW__UserI__282DF8C2");
        });

        modelBuilder.Entity<TblAuction>(entity =>
        {
            entity.HasKey(e => e.AuctionId).HasName("PK__tblAucti__51004A4C8D65AE77");

            entity.ToTable("tblAuctions");

            entity.HasIndex(e => e.AuctionNumber, "UQ__tblAucti__C90DD8B8624ABC12").IsUnique();

            entity.Property(e => e.AuctionNumber).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndDateTime).HasColumnType("datetime");
            entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasDefaultValue("Auction");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.TblAuctions)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__tblAuctio__Categ__70DDC3D8");
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

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<TblAuctionStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__tblAucti__C8EE20639F7FF226");

            entity.ToTable("tblAuctionStatuses");

            entity.Property(e => e.Name).HasMaxLength(50);
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

        modelBuilder.Entity<TblCountry>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__tblCount__10D1609F81F3F3B3");

            entity.ToTable("tblCountries");

            entity.Property(e => e.CountryName).HasMaxLength(100);
            entity.Property(e => e.MaxLength).HasDefaultValue(10);
            entity.Property(e => e.MinLength).HasDefaultValue(10);
            entity.Property(e => e.PhoneCode).HasMaxLength(10);
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

        modelBuilder.Entity<TblRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__tblRoles__8AFACE1A5E190086");

            entity.ToTable("tblRoles");

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<TblSeller>(entity =>
        {
            entity.HasKey(e => e.SellerId).HasName("PK__tblSelle__7FE3DB81818038FE");

            entity.ToTable("tblSellers");

            entity.HasOne(d => d.User).WithMany(p => p.TblSellers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__tblSeller__UserI__02FC7413");
        });

        modelBuilder.Entity<TblStaticPagesSetting>(entity =>
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

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tblUsers__1788CC4CA0D5030C");

            entity.ToTable("tblUsers");

            entity.HasIndex(e => e.Uid, "UQ__tblUsers__C5B19603F27D43C6").IsUnique();

            entity.Property(e => e.ChatEnabled).HasDefaultValue(true);
            entity.Property(e => e.CompanyName).HasMaxLength(255);
            entity.Property(e => e.CompanyNumber).HasMaxLength(50);
            entity.Property(e => e.Deposit)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.LastOnline).HasColumnType("datetime");
            entity.Property(e => e.MobileNumber).HasMaxLength(4);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PersonalIdImage).HasMaxLength(255);
            entity.Property(e => e.PersonalIdNumber).HasMaxLength(100);
            entity.Property(e => e.ProfileImage).HasMaxLength(255);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TotalLimit)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Uid).HasColumnName("UID");

            entity.HasOne(d => d.Country).WithMany(p => p.TblUsers)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_tblUsers_CountryId");

            entity.HasOne(d => d.Status).WithMany(p => p.TblUsers)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblUsers__Status__300424B4");
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

            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<TblVatoption>(entity =>
        {
            entity.HasKey(e => e.Vatid).HasName("PK__tblVATOp__4A96282EABE16FDE");

            entity.ToTable("tblVATOptions");

            entity.HasIndex(e => e.Vattype, "UQ__tblVATOp__68B32E85E8A08FFD").IsUnique();

            entity.Property(e => e.Vatid).HasColumnName("VATId");
            entity.Property(e => e.Vattype)
                .HasMaxLength(50)
                .HasColumnName("VATType");
        });

        modelBuilder.Entity<TblWinnerAwardingOption>(entity =>
        {
            entity.HasKey(e => e.AwardingId).HasName("PK__tblWinne__F43393FB4EEF2A31");

            entity.ToTable("tblWinnerAwardingOptions");

            entity.HasIndex(e => e.AwardingMethod, "UQ__tblWinne__5B5DB46B5BED39DB").IsUnique();

            entity.Property(e => e.AwardingMethod).HasMaxLength(50);
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
                .HasConstraintName("FK__tblWinner__Winne__2CF2ADDF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
