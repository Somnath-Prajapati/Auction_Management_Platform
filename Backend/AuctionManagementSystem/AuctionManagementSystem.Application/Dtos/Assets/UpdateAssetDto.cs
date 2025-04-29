using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class UpdateAssetDto
    {
        public int AssetId { get; set; }

        public string Title { get; set; } = null!;

        public int? CategoryId { get; set; }

        public decimal? Deposit { get; set; }

        public int SellerId { get; set; }

        public decimal? Commission { get; set; }

        public decimal StartingPrice { get; set; }

        public decimal? ReserveAmount { get; set; }

        public int? IncrementalTime { get; set; }

        public decimal? MinIncrement { get; set; }

        public bool? MakeOffer { get; set; }

        public bool? Featured { get; set; }

        public int? AwardingId { get; set; }

        public int? StatusId { get; set; }

        public int? Vatid { get; set; }

        public decimal? Vatpercent { get; set; }

        public string? CourtCaseNumber { get; set; }

        public int? RegistrationDeadline { get; set; }

        public string? Description { get; set; }

        public decimal? MapLatitude { get; set; }

        public decimal? MapLongitude { get; set; }

        public decimal? AdminFees { get; set; }

        public decimal? AuctionFees { get; set; }

        public decimal? BuyerCommission { get; set; }

        public int? WinnerId { get; set; }

        public string? SalesNotes { get; set; }

        //public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }
}
