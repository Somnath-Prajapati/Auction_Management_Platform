using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Dtos.Auctions;
using AuctionManagementSystem.Application.Dtos.Requests;
using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using AuctionManagementSystem.Application.Features.Requests.Command.AddRequest;
using AuctionManagementSystem.Application.Features.Requests.Command.UpdRequest;
using AuctionManagementSystem.Application.Dtos.UserDtos;
using AuctionManagementSystem.Application.Features.Auctions.Commands.CreateAuction;
using AuctionManagementSystem.Application.Features.Auctions.Commands.UpdateAuction;
using AuctionManagementSystem.Application.Features.Requests.Command.AddRequest;
using AuctionManagementSystem.Application.Features.Requests.Command.UpdRequest;
using AuctionManagementSystem.Application.Features.Settings.DirectSaleSettings.Commands.CreateDirectSaleSettings;
using AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.CreateFinanceSettings;
using AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Command.CreateFooterLinksSettings;
using AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Command.UpdateFooterLinksSettings;
using AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Command.CreateStaticPagesSettings;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Auction;
using AuctionManagementSystem.Domain.Entities.Request;
using AuctionManagementSystem.Domain.Entities.Settings;
using AuctionManagementSystem.Domain.Entities.Transaction;
using AuctionManagementSystem.Domain.Entities.User;
using AutoMapper;
using EventStore.ClientAPI;


namespace AuctionManagementSystem.Application.Profiles
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<TblUser,UserDto>().ReverseMap();
            CreateMap<TblUser, GetUserDto>().ReverseMap();
            CreateMap<TblSystemSetting, SystemSettingsDto>().ReverseMap();
            CreateMap<SystemSettings, SystemSettingsDto>().ReverseMap();
            // Mapping from Entity to DTO (FinanceSettingsDto)
            CreateMap<CreateFinanceSettingsCommand, TblFinanceSetting>();
            CreateMap<TblFinanceSetting, FinanceSettingsDto>();
            // Mapping from Entity to DTO (dirctSaleSettingsDto)
            CreateMap<TblDirectSaleSetting, DirectSaleSettingsDto>();
            CreateMap<CreateDirectSaleSettingsCommand, TblDirectSaleSetting>();
            // Mapping from Entity to DTO (FooterLinksSettingDto)
            CreateMap<CreateFooterLinksSettingsCommand, TblFooterLinksSetting>().ReverseMap();
            CreateMap<UpdateFooterLinksSettingsCommand, TblFooterLinksSetting>().ReverseMap();
            CreateMap<TblFooterLinksSetting, FooterLinksSettingsDto>().ReverseMap();
            // Mapping from Entity to DTO (StaticPageSettingsDto)
            CreateMap<TblStaticPagesSetting, StaticPagesSettingsDto>().ReverseMap();
            CreateMap<CreateStaticPagesSettingsCommand, TblStaticPagesSetting>().ReverseMap();
            CreateMap<StaticPagesSettingsDto, TblStaticPagesSetting>().ReverseMap();
            //Transactions Mappping
            CreateMap<TblTransaction, TransactionDto>().ReverseMap();
            CreateMap<CreateTransactionDto, TblTransaction>();
            // Update
            CreateMap<UpdateTransactionDto, TblTransaction>().ForMember(dest => dest.TransactionId, opt => opt.Ignore()); // ID shouldn't be overwritten

            //Mapping for Create,Upadte Request Dto
            CreateMap<CreateRequestDto, AddRequestCommand>();
            CreateMap<UpdateRequestDto, UdpRequestCommand>();
            CreateMap<TblRequest, RequestDto>();
            


            CreateMap<GetAssetsDto, TblAsset>().ReverseMap();

            CreateMap<CreateAuctionCommand, TblAuction>()
            .IncludeBase<AuctionBaseCommand, TblAuction>();  // Include the common properties from AuctionBaseCommand

            CreateMap<TblAssetCategory, AssetCategoryDto>().ReverseMap();
            CreateMap<CreateAssetCategoryDto, TblAssetCategory>();
            CreateMap<UpdateAssetCategoryDto, TblAssetCategory>();

            CreateMap<CreateAssetsDto, TblAsset>().ReverseMap();

            CreateMap<UpdateAssetDto, TblAsset>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => false)); 

            CreateMap<AssetsGalleryDto, TblAssetGallery>().ReverseMap();
            CreateMap<AuctionBaseCommand, TblAuction>()
            .ForMember(dest => dest.AuctionId, opt => opt.Ignore());
            CreateMap<AssetsGalleryDto , TblAssetGallery>().ReverseMap();

            CreateMap<AssetsGalleryDto, TblAssetGallery>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<AssetGalleryDtos, TblAssetGallery>().ReverseMap();

            CreateMap<AssetGalleryDtos, TblAssetGallery>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<AssetDocumentDto, TblAssetDocument>().ReverseMap();
            CreateMap<AssetDocumentUploadDto, TblAssetDocument>().ReverseMap();

            CreateMap<GetAssetsFormDto, TblAsset>().ReverseMap();

            CreateMap<AssetDocumentFormDto, TblAssetDocument>().ReverseMap();

            CreateMap<AssetDetailDto, TblAssetDetail>().ReverseMap();

            CreateMap<AssetDetailDto, TblAssetDetail>().ReverseMap();
            //Mapping for Create,Upadte Request Dto
            CreateMap<CreateRequestDto, AddRequestCommand>();
            CreateMap<UpdateRequestDto, UdpRequestCommand>();
            CreateMap<TblRequest, RequestDto>();
            //CreateMap<CreateRequestDto, TblRequest>().ReverseMap();



            CreateMap<CreateRequestDto, AddRequestCommand>().ReverseMap();
            CreateMap<TblRequest, CreateRequestDto>().ReverseMap();


        }
    }
}
