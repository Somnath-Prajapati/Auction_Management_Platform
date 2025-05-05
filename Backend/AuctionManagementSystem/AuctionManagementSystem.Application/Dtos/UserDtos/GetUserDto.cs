using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.UserDtos
{
    public class GetUserDto
    {
        public int UserId { get; set; }
        public int Uid { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;
        public string? CompanyName { get; set; }
        public string? CompanyNumber { get; set; }
        public int StatusId { get; set; }
        public bool ChatEnabled { get; set; } = true;
        public int RoleId { get; set; }
        public string PersonalIdNumber { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateOnly PersonalIdExpiryDate { get; set; }
        public int CountryId { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? PersonalIdImageUrl { get; set; }

        public decimal? TotalLimit { get; set; }
        public decimal? Deposit { get; set; }
    }
}
