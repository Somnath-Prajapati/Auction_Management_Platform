namespace AuctionManagementSystem.Application.DTOs.Assets.AssetCategory
{
    public class CreateAssetCategoryDto
    {
        public string CategoryName { get; set; }
        public string? Subcategory { get; set; }
        public decimal DepositPercentage { get; set; }
        public string? Details { get; set; }
        public decimal AdminFees { get; set; }
        public decimal AuctionFees { get; set; }
        public decimal BuyerCommission { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public string? Icon { get; set; }
        public decimal? Vatpercentage { get; set; }
        public int StatusId { get; set; }
        public int? Vatid { get; set; }
    }
}
