using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Persistence.TempEntities;

public partial class TblFooterLinksSetting
{
    public int Id { get; set; }

    public string? Faq { get; set; }

    public string? Blog { get; set; }

    public string? Status { get; set; }

    public string? TwitterLink { get; set; }

    public string? InstagramLink { get; set; }

    public string? FacebookLink { get; set; }

    public string? LinkedInLink { get; set; }

    public string? YouTubeLink { get; set; }

    public string? AppStoreLink { get; set; }

    public string? GooglePlayLink { get; set; }
}
