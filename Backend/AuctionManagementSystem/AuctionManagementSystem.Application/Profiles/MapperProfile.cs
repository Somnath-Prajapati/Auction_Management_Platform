using System.Data;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Dtos.Auctions;
using AuctionManagementSystem.Application.Dtos.AuditTrial;
using AuctionManagementSystem.Application.Dtos.Bids;
using AuctionManagementSystem.Application.Dtos.Notification;
using AuctionManagementSystem.Application.Dtos.Requests;
using AuctionManagementSystem.Application.Dtos.Roles;
using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using AuctionManagementSystem.Application.Dtos.UserDtos;
using AuctionManagementSystem.Application.Features.Auctions.Commands.CreateAuction;
using AuctionManagementSystem.Application.Features.Bids.CreateBid.Command;
using AuctionManagementSystem.Application.Features.Requests.Command.AddRequest;
using AuctionManagementSystem.Application.Features.Requests.Command.UpdRequest;
using AuctionManagementSystem.Application.Features.Settings.DirectSaleSettings.Commands.CreateDirectSaleSettings;
using AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.CreateFinanceSettings;
using AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.UpdateFinanceSettings;
using AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Command.CreateFooterLinksSettings;
using AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Command.UpdateFooterLinksSettings;
using AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Command.CreateStaticPagesSettings;
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
using AuctionManagementSystem.Domain.Entities.User;
using AutoMapper;
using EventStore.ClientAPI;


namespace AuctionManagementSystem.Application.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<TblUser, UserDto>().ReverseMap();
            CreateMap<TblNotification, NotificationDto>().ReverseMap();
            CreateMap<TblUser, GetUserDto>().ReverseMap();
            CreateMap<TblAssetWinner, AssetWinnerDto>().ReverseMap();
            CreateMap<TblSystemSetting, SystemSettingsDto>().ReverseMap();
            CreateMap<SystemSettings, SystemSettingsDto>().ReverseMap();
            // Mapping from Entity to DTO (FinanceSettingsDto)
            CreateMap<CreateFinanceSettingsCommand, TblFinanceSetting>();
            CreateMap<UpdateFinanceSettingsCommand, TblFinanceSetting>();
            CreateMap<TblFinanceSetting, FinanceSettingsDto>().ReverseMap();

            // Mapping from Entity to DTO (dirctSaleSettingsDto)
            CreateMap<TblDirectSaleSetting, DirectSaleSettingsDto>().ReverseMap();
            CreateMap<CreateDirectSaleSettingsCommand, TblDirectSaleSetting>();
            // Mapping from Entity to DTO (FooterLinksSettingDto)
            CreateMap<CreateFooterLinksSettingsCommand, TblFooterLinksSetting>().ReverseMap();
            CreateMap<UpdateFooterLinksSettingsCommand, TblFooterLinksSetting>().ReverseMap();
            CreateMap<TblFooterLinksSetting, FooterLinksSettingsDto>().ReverseMap();
            // Mapping from Entity to DTO (StaticPageSettingsDto)
            CreateMap<TblStaticPagesSettingDto, StaticPagesSettingsDto>().ReverseMap();
            CreateMap<CreateStaticPagesSettingsCommand, TblStaticPagesSettingDto>().ReverseMap();
            CreateMap<StaticPagesSettingsDto, TblStaticPagesSettingDto>().ReverseMap();
            //Transactions Mappping
            //CreateMap<TblTransaction, TransactionDto>().ReverseMap();
            CreateMap<CreateTransactionDto, TblTransaction>();
            // Update
            CreateMap<UpdateTransactionDto, TblTransaction>().ForMember(dest => dest.TransactionId, opt => opt.Ignore()); // ID shouldn't be overwritten

            //Mapping for Create,Upadte Request Dto
            CreateMap<CreateRequestDto, AddRequestCommand>();
            CreateMap<UpdateRequestDto, UdpRequestCommand>();
            CreateMap<TblRequest, RequestDto>();



            CreateMap<GetAssetsDto, TblAsset>().ReverseMap();
            CreateMap<GetAssetsFormDto, DirectSaleAssetDto>();


            CreateMap<CreateAuctionCommand, TblAuction>()
            .IncludeBase<AuctionBaseCommand, TblAuction>();  // Include the common properties from AuctionBaseCommand
            CreateMap<TblAuditTrail, AuditTrailDto>().ReverseMap();
            CreateMap<TblAssetCategory, AssetCategoryDto>().ReverseMap();
            CreateMap<CreateAssetCategoryDto, TblAssetCategory>();
            CreateMap<UpdateAssetCategoryDto, TblAssetCategory>();

            CreateMap<CreateAssetsDto, TblAsset>().ReverseMap();

            CreateMap<UpdateAssetDto, TblAsset>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false));

            CreateMap<AssetsGalleryDto, TblAssetGallery>().ReverseMap();
            CreateMap<AuctionBaseCommand, TblAuction>()
            .ForMember(dest => dest.AuctionId, opt => opt.Ignore());
            CreateMap<AssetsGalleryDto, TblAssetGallery>().ReverseMap();

            CreateMap<AssetsGalleryDto, TblAssetGallery>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<AssetGalleryDtos, TblAssetGallery>().ReverseMap();

            CreateMap<AssetGalleryDtos, TblAssetGallery>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<AssetDocumentDto, TblAssetDocument>().ReverseMap();
            CreateMap<AssetDocumentUploadDto, TblAssetDocument>().ReverseMap();

            CreateMap<AssetDocumentFormDto, TblAssetDocument>().ReverseMap();
            CreateMap<GetAssetDetailsDto, TblAssetDetail>().ReverseMap();
            //Mapping for Create,Upadte Request Dto
            CreateMap<CreateRequestDto, AddRequestCommand>();
            CreateMap<UpdateRequestDto, UdpRequestCommand>();
            CreateMap<TblRequest, RequestDto>();
            CreateMap<AddBidCommand, tblBid>()
            .ForMember(dest => dest.AuctionId, opt => opt.MapFrom(src => src.AuctionId))
            .ForMember(dest => dest.AssetId, opt => opt.MapFrom(src => src.AssetId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.BidAmount, opt => opt.MapFrom(src => src.BidAmount)).ReverseMap();


            CreateMap<TblTransaction, TransactionDto>()
           .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.Name))
           .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType.TransactionTypeName))
           .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod.PaymentMethodName))
           .ForMember(dest => dest.CardType, opt => opt.MapFrom(src => src.CardType != null ? src.CardType.CardTypeName : null))
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.StatusName))
           .ForMember(dest => dest.DocumentUrl, opt => opt.MapFrom(src =>
               src.TblTransactionDocuments.Select(d => d.FilePath ?? "").ToList()
           ));

            CreateMap<TblTransaction, GetTransactionDto>()
             .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom(src => src.PaymentMethod.PaymentMethodName))
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.StatusName))
            .ForMember(dest => dest.TransactionTypeName, opt => opt.MapFrom(src => src.TransactionType.TransactionTypeName))
            .ForMember(dest => dest.CardTypeName, opt => opt.MapFrom(src => src.CardType != null ? src.CardType.CardTypeName : null));

            CreateMap<TblCardType, CardTypeDto>();
            CreateMap<TblTransactionType, TransactionTypeDto>();
            CreateMap<TblPaymentMethod, PaymentMethodDto>();
            CreateMap<TblTransactionStatus, TransactionStatusDto>();

            CreateMap<GetAssetsFormDto, DirectSaleAssetDto>()
           .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.StartingPrice)) // If StartingPrice is null, default to 0
           .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src =>
               src.Galleries.OrderBy(g => g.SortOrder).Select(g => g.FilePath).FirstOrDefault() ?? null)) // Get the first gallery image as the thumbnail URL
           .ForMember(dest => dest.IsAvailableForDirectSale, opt => opt.MapFrom(src => src.IsAvailableForDirectSale))
           .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName)) // Map category name
           .ReverseMap();

            //CreateMap<CreateRequestDto, TblRequest>().ReverseMap();

            CreateMap<TblAsset, DirectSaleAssetDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                //.ForMember(dest => dest., opt => opt.MapFrom(src => src.Seller.Name))
                // Add more as needed
                ;


            CreateMap<AddAutoBidDto, TblAutoBid>().ReverseMap();

            CreateMap<CreateRequestDto, AddRequestCommand>().ReverseMap();
            CreateMap<TblRequest, CreateRequestDto>().ReverseMap();
            CreateMap<TblCartItem, DirectSaleAssetDto>()
                .ForMember(dest => dest.AssetId, opt => opt.MapFrom(src => src.Asset.AssetId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Asset.Title))
    // ... map other asset fields from src.Asset
    ;
            CreateMap<WonBidDto, TblAssetWinner>().ReverseMap();
            CreateMap<TblRole, RoleWithPermissionsDto>()
               .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
               .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName))
               .ForMember(dest => dest.IsSeller, opt => opt.MapFrom(src => src.IsSeller))
               .ForMember(dest => dest.SuperAdmin, opt => opt.MapFrom(src => src.TblRolePermissionsMatrices.FirstOrDefault() != null ? src.TblRolePermissionsMatrices.FirstOrDefault().SuperAdmin ?? false : false))
               .ForMember(dest => dest.AccessAdminPanel, opt => opt.MapFrom(src => src.TblRolePermissionsMatrices.FirstOrDefault() != null ? src.TblRolePermissionsMatrices.FirstOrDefault().AccessAdminPanel ?? false : false))
               .ForMember(dest => dest.ManageAuctions, opt => opt.MapFrom(src => src.TblRolePermissionsMatrices.FirstOrDefault() != null ? src.TblRolePermissionsMatrices.FirstOrDefault().ManageAuctions ?? false : false))
               .ForMember(dest => dest.ManageAssets, opt => opt.MapFrom(src => src.TblRolePermissionsMatrices.FirstOrDefault() != null ? src.TblRolePermissionsMatrices.FirstOrDefault().ManageAssets ?? false : false))
               .ForMember(dest => dest.ManageTransactions, opt => opt.MapFrom(src => src.TblRolePermissionsMatrices.FirstOrDefault() != null ? src.TblRolePermissionsMatrices.FirstOrDefault().ManageTransactions ?? false : false))
               .ForMember(dest => dest.ManageCategories, opt => opt.MapFrom(src => src.TblRolePermissionsMatrices.FirstOrDefault() != null ? src.TblRolePermissionsMatrices.FirstOrDefault().ManageCategories ?? false : false))
               .ForMember(dest => dest.ManageRoles, opt => opt.MapFrom(src => src.TblRolePermissionsMatrices.FirstOrDefault() != null ? src.TblRolePermissionsMatrices.FirstOrDefault().ManageRoles ?? false : false))
               .ForMember(dest => dest.ManageUsers, opt => opt.MapFrom(src => src.TblRolePermissionsMatrices.FirstOrDefault() != null ? src.TblRolePermissionsMatrices.FirstOrDefault().ManageUsers ?? false : false))
               .ForMember(dest => dest.ViewReports, opt => opt.MapFrom(src => src.TblRolePermissionsMatrices.FirstOrDefault() != null ? src.TblRolePermissionsMatrices.FirstOrDefault().ViewReports ?? false : false))
               .ForMember(dest => dest.ExportReports, opt => opt.MapFrom(src => src.TblRolePermissionsMatrices.FirstOrDefault() != null ? src.TblRolePermissionsMatrices.FirstOrDefault().ExportReports ?? false : false))
               .ForMember(dest => dest.ManageRequests, opt => opt.MapFrom(src => src.TblRolePermissionsMatrices.FirstOrDefault() != null ? src.TblRolePermissionsMatrices.FirstOrDefault().ManageRequests ?? false : false))
               .ForMember(dest => dest.ViewAuditTrail, opt => opt.MapFrom(src => src.TblRolePermissionsMatrices.FirstOrDefault() != null ? src.TblRolePermissionsMatrices.FirstOrDefault().ViewAuditTrail ?? false : false))
               .ForMember(dest => dest.ChangeCommission, opt => opt.MapFrom(src => src.TblRolePermissionsMatrices.FirstOrDefault() != null ? src.TblRolePermissionsMatrices.FirstOrDefault().ChangeCommission ?? false : false));

            // DTO -> Entity for creating/updating Role
            CreateMap<RoleWithPermissionsDto, TblRole>()
                .ForMember(dest => dest.RoleId, opt => opt.Ignore()) // RoleId assigned by DB
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName))
                .ForMember(dest => dest.IsSeller, opt => opt.MapFrom(src => src.IsSeller));

            // DTO -> Entity for creating/updating PermissionsMatrix
            CreateMap<RoleWithPermissionsDto, TblRolePermissionsMatrix>()
                .ForMember(dest => dest.RoleId, opt => opt.Ignore()) // Set explicitly in code
                .ForMember(dest => dest.SuperAdmin, opt => opt.MapFrom(src => src.SuperAdmin))
                .ForMember(dest => dest.AccessAdminPanel, opt => opt.MapFrom(src => src.AccessAdminPanel))
                .ForMember(dest => dest.ManageAuctions, opt => opt.MapFrom(src => src.ManageAuctions))
                .ForMember(dest => dest.ManageAssets, opt => opt.MapFrom(src => src.ManageAssets))
                .ForMember(dest => dest.ManageTransactions, opt => opt.MapFrom(src => src.ManageTransactions))
                .ForMember(dest => dest.ManageCategories, opt => opt.MapFrom(src => src.ManageCategories))
                .ForMember(dest => dest.ManageRoles, opt => opt.MapFrom(src => src.ManageRoles))
                .ForMember(dest => dest.ManageUsers, opt => opt.MapFrom(src => src.ManageUsers))
                .ForMember(dest => dest.ViewReports, opt => opt.MapFrom(src => src.ViewReports))
                .ForMember(dest => dest.ExportReports, opt => opt.MapFrom(src => src.ExportReports))
                .ForMember(dest => dest.ManageRequests, opt => opt.MapFrom(src => src.ManageRequests))
                .ForMember(dest => dest.ViewAuditTrail, opt => opt.MapFrom(src => src.ViewAuditTrail))
                .ForMember(dest => dest.ChangeCommission, opt => opt.MapFrom(src => src.ChangeCommission));
        }
    }

}


