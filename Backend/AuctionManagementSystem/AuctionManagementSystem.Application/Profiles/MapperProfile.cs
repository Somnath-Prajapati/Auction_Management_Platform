using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using AuctionManagementSystem.Application.Dtos.UserDtos;
using AuctionManagementSystem.Application.Features.Settings.DirectSaleSettings.Commands.CreateDirectSaleSettings;
using AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.CreateFinanceSettings;
using AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Command.CreateFooterLinksSettings;
using AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Command.UpdateFooterLinksSettings;
using AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Command.CreateStaticPagesSettings;
using AuctionManagementSystem.Domain.Entities.Asset;
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


            CreateMap<GetAssetsDto, TblAsset>().ReverseMap();


            CreateMap<CreateAssetsDto, TblAsset>().ReverseMap();

            CreateMap<UpdateAssetDto, TblAsset>().ReverseMap();

        }
    }
}
