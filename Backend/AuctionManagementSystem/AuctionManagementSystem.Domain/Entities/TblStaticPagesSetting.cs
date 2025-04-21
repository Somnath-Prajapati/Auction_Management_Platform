using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Persistence.TempEntities;

public partial class TblStaticPagesSetting
{
    public int Id { get; set; }

    public string? PrivacyPolicy { get; set; }

    public string? TermsAndConditions { get; set; }

    public string? CookiesPolicy { get; set; }
}
