using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Payment
{
    public class CheckStripePaymentStatusDto
    {
        public string SessionId { get; set; }
    }


    public class CreateStripeSessionDto
    {
        public int UserId { get; set; }
        public string email { get; set; }
        public List<int> AssetIds { get; set; } = new();
        public decimal TotalAmount { get; set; }
    }

    public class ConfirmStripePaymentDto
    {
        public string SessionId { get; set; }
        public int UserId { get; set; }
    }


}
