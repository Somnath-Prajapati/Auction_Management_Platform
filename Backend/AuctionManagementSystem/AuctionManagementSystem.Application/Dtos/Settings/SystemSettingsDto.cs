using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Settings
{
    public class SystemSettingsDto
    {
        public int Id { get; set; }
        public int? GlobalIncrementalTimeInMinutes { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
