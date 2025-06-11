using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.TransactionsDtos
{
    public class DirectSaleMonthlyRevenueDto
    {
        public int Year { get; set; }
        public int? Month { get; set; } // nullable for yearly
        public decimal TotalAmount { get; set; }
    }

    public class AuctionMonthlyRevenueDto
    {
        public int Year { get; set; }
        public int? Month { get; set; } // nullable for yearly
        public decimal TotalAmount { get; set; }
    }







}
