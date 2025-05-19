using Microsoft.AspNetCore.Http;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class UpdateAssetCategoryDto
    {
        public string? CategoryName { get; set; }
        public string? Subcategory { get; set; }
        public decimal DepositPercentage { get; set; }
        public string? Details { get; set; }
        public decimal AdminFees { get; set; }
        public decimal AuctionFees { get; set; }
        public decimal BuyerCommission { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public string? Icon { get; set; }
        public IFormFile? IconFile { get; set; } // actual uploaded file

        public IFormFile? Document { get; set; } //to upload the files/documents
        public decimal? Vatpercentage { get; set; }
        public int StatusId { get; set; }
        public int? Vatid { get; set; }

        public List<int> PaymentMethodIds { get; set; } = new List<int>();
    }
}
