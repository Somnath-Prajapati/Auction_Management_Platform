using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class AssetTransactionDto
    {
        public int AssetId { get; set; }
        //public int OrderId { get; set; }
        public string TransactionNumber { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDateTime { get; set; }
        //public string OrderNumber { get; set; }
                                        // Add more properties as needed
    }

}
