using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Settings
{
    public class StaticPagesSettingsDto
    {
        public int Id { get; set; }
        public string? PrivacyPolicy { get; set; }
        public string? TermsAndConditions { get; set; }
        public string? CookiesPolicy { get; set; }
    }
}
