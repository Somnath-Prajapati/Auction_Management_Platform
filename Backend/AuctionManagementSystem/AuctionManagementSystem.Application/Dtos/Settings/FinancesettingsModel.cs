using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Settings
{
    public class FinancesettingsModel
    {
        public int Id { get; set; }
        public decimal VATPercent { get; set; }
        public decimal CreditCardFee { get; set; }
        public decimal DebitCardFee { get; set; }
        public decimal AdminFees { get; set; }
        public decimal AuctionFees { get; set; }
        public decimal BuyerCommissionPercent { get; set; }
    }
}
