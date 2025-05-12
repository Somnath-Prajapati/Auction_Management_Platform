using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class AssetCreateDto
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public decimal DepositPercentage { get; set; }
        public string SellerId { get; set; }
        public decimal CommissionPercentage { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal ReserveAmount { get; set; }
        public string IncrementalTime { get; set; } // e.g., "5 Minutes"
        public decimal MinimumIncrement { get; set; }
        public bool MakeOffer { get; set; }
        public bool IsFeatured { get; set; }
        public string WinnerAwarding { get; set; } // "Automatic" / "Manual"
        public bool DeliveryRequired { get; set; }
        public string Status { get; set; } // e.g., "Draft"

        public string VatOption { get; set; } // e.g., "Ex./Inc./NA"
        public decimal VatPercentage { get; set; }

        public string CourtCaseNumber { get; set; }
        public string RegistrationDeadline { get; set; } // e.g., "30 days"

        public bool RequestForViewing { get; set; }
        public bool RequestForInquiry { get; set; }

        public decimal AdminFees { get; set; }
        public decimal AuctionFees { get; set; }
        public decimal BuyerCommissionPercentage { get; set; }  


        public List<AssetDetailDto> Details { get; set; }


        public List<IFormFile> GalleryFiles { get; set; } = new List<IFormFile>();
        public List<IFormFile> DocumentFiles { get; set; } = new List<IFormFile>();
    }

}
