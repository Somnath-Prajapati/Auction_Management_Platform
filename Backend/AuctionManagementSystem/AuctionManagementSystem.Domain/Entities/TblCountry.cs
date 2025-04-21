using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Domain.Entities;

public partial class TblCountry
{
    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public string PhoneCode { get; set; } = null!;

    public int MinLength { get; set; }

    public int MaxLength { get; set; }

    public virtual ICollection<TblUser> TblUsers { get; set; } = new List<TblUser>();
}
