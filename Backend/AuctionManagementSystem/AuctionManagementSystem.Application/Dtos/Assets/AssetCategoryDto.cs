using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class AssetCategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Subcategory { get; set; }
        public decimal DepositPercentage { get; set; }
        public string Details { get; set; }
        public decimal AdminFees { get; set; }
        public decimal AuctionFees { get; set; }
        public decimal BuyerCommission { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public string Icon { get; set; }
        public string Document { get; set; }
        public decimal? Vatpercentage { get; set; }
        public int StatusId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int? Vatid { get; set; }
    }

}
