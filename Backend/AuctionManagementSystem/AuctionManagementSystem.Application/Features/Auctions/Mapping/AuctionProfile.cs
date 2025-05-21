using AuctionManagementSystem.Application.Dtos.Auctions;
using AuctionManagementSystem.Application.Features.Auctions.Commands.CreateAuction;
using AuctionManagementSystem.Application.Features.Auctions.Commands.UpdateAuction;
using AuctionManagementSystem.Domain.Entities.Auction;
using AutoMapper;

namespace AuctionManagementSystem.Application.Features.Auctions.Mapping
{
    public class AuctionProfile : Profile
    {
        public AuctionProfile()
        {
            // Map TblAuction to AuctionDto
            CreateMap<TblAuction, AuctionDto>()
                .ForMember(dest => dest.CategoryName, opt =>
                    opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : string.Empty))
                .ForMember(dest => dest.StatusName, opt =>
                    opt.MapFrom(src => src.Status != null ? src.Status.Name : string.Empty))
                .ReverseMap();

            // CreateAuctionCommand to TblAuction
            CreateMap<CreateAuctionCommand, TblAuction>()
                .ForMember(dest => dest.AuctionNumber, opt => opt.MapFrom(src => src.AuctionNumber))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.StartDateTime, opt => opt.MapFrom(src => src.StartDateTime))
                .ForMember(dest => dest.EndDateTime, opt => opt.MapFrom(src => src.EndDateTime))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.IncrementalTime, opt => opt.MapFrom(src => src.IncrementalTime))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore()); // ensure IsDeleted isn't accidentally mapped

            // UpdateAuctionCommand to TblAuction
            CreateMap<UpdateAuctionCommand, TblAuction>()
                .ForMember(dest => dest.AuctionId, opt => opt.MapFrom(src => src.AuctionId))
                .ForMember(dest => dest.AuctionNumber, opt => opt.MapFrom(src => src.AuctionNumber))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.StartDateTime, opt => opt.MapFrom(src => src.StartDateTime))
                .ForMember(dest => dest.EndDateTime, opt => opt.MapFrom(src => src.EndDateTime))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.IncrementalTime, opt => opt.MapFrom(src => src.IncrementalTime))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore()); // preserve soft delete flag
        }
    }
}
