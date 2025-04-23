using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos;
using AuctionManagementSystem.Domain.Entities;
using AutoMapper;

namespace AuctionManagementSystem.Application.Profiles
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<TblUser,UserDto>().ReverseMap();
            CreateMap<TblUser, GetUserDto>().ReverseMap();
        }
    }
}
