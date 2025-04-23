namespace AuctionManagementSystem.Application.Dtos.Settings
{
    public class DirectSaleSettingsDto
    {
        public int Id { get; set; }
        public int CartItemsLimit { get; set; }
        public int CartTimerInMinutes { get; set; }
        public decimal MinimumBidAmount { get; set; }
        public decimal MinimumReservePrice { get; set; }
        public decimal MaximumDiscountAmount { get; set; }
    }
}
