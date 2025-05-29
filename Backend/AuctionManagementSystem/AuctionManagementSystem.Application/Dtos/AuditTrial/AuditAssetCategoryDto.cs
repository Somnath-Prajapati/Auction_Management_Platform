using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.AuditTrial
{
    public class AuditAssetCategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? Icon { get; set; }
        public string? DocumentPath { get; set; }
        public bool IsDeleted { get; set; }
        public List<int> PaymentMethodIds { get; set; } 
    }
}
