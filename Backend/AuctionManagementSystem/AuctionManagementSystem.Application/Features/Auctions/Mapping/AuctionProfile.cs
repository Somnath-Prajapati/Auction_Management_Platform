using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Auctions;
using AuctionManagementSystem.Application.Features.Auctions.Commands.CreateAuction;
using AuctionManagementSystem.Application.Features.Auctions.Commands.UpdateAuction;
using AuctionManagementSystem.Domain.Entities;
using AuctionManagementSystem.Domain.Entities.Auction;
using AutoMapper;

namespace AuctionManagementSystem.Application.Features.Auctions.Mapping
{
    public class AuctionProfile : Profile
    {
        public AuctionProfile()
        {
            CreateMap<TblAuction, AuctionDto>().ReverseMap();

            CreateMap<CreateAuctionCommand, TblAuction>()
                .ForMember(dest => dest.AuctionNumber, opt => opt.MapFrom(src => src.AuctionNumber))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.StartDateTime, opt => opt.MapFrom(src => src.StartDateTime))
                .ForMember(dest => dest.EndDateTime, opt => opt.MapFrom(src => src.EndDateTime))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.IncrementalTime, opt => opt.MapFrom(src => src.IncrementalTime))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

            CreateMap<UpdateAuctionCommand, TblAuction>()
              .ForMember(dest => dest.AuctionId, opt => opt.MapFrom(src => src.AuctionId))
              .ForMember(dest => dest.AuctionNumber, opt => opt.MapFrom(src => src.AuctionNumber))
              .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
              .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
              .ForMember(dest => dest.StartDateTime, opt => opt.MapFrom(src => src.StartDateTime))
              .ForMember(dest => dest.EndDateTime, opt => opt.MapFrom(src => src.EndDateTime))
              .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
              .ForMember(dest => dest.IncrementalTime, opt => opt.MapFrom(src => src.IncrementalTime))
              .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));
        }
    }
}
